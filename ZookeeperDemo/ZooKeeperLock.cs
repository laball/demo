using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using org.apache.zookeeper;
using static org.apache.zookeeper.ZooDefs;

namespace ZookeeperDemo
{

    //* 来源java网络源码的zookeeper分布式锁实现（目前仅翻译并简单测试ok，未来集成入sdk）
    //* 备注:
    //  共享锁在同一个进程中很容易实现，但是在跨进程或者在不同 Server 之间就不好实现了。Zookeeper 却很容易实现这个功能，实现方式也是需要获得锁的 Server 创建一个 EPHEMERAL_SEQUENTIAL 目录节点，
    //  然后调用 getChildren方法获取当前的目录节点列表中最小的目录节点是不是就是自己创建的目录节点，如果正是自己创建的，那么它就获得了这个锁，
    //  如果不是那么它就调用 exists(String path, boolean watch) 方法并监控 Zookeeper 上目录节点列表的变化，一直到自己创建的节点是列表中最小编号的目录节点，
    //  从而获得锁，释放锁很简单，只要删除前面它自己所创建的目录节点就行了。  

    /// <summary>
    /// 原始代码来源：http://www.cnblogs.com/chejiangyi/p/4938400.html
    /// </summary>
    public class ZooKeeperLock : Watcher, IDistributedLock
    {
        private ZooKeeper zk;

        private string rootNode = "/locks"; //根

        private string config;
        private string lockName; //竞争资源的标志
        private string preWaitNode; //等待前一个锁
        private string currentNode; //当前锁

        private AutoResetEvent lockResetEvent;
        private AutoResetEvent connectionResetEvent;

        private const int AnyVersion = -1;
        private const int SessionTimeout = 30 * 1000;
        private static readonly byte[] EmptyData = new byte[0];

        /// <summary>
        /// 创建分布式锁,使用前请确认config配置的zookeeper服务可用 </summary>
        /// <param name="zkConfig"> 127.0.0.1:2181 </param>
        /// <param name="lockName"> 竞争资源标志,lockName中不能包含单词lock </param>
        public ZooKeeperLock(string zkConfig, string lockName)
        {
            this.lockName = lockName;
            config = zkConfig;

            Init();
        }

        public ZooKeeperLock(string zkConfig, string lockRootName, string lockName)
        {
            config = zkConfig;
            this.lockName = lockRootName;
            this.lockName = lockName;

            Init();
        }

        private void Init()
        {
            try
            {
                connectionResetEvent = new AutoResetEvent(false);

                zk = new ZooKeeper(config, SessionTimeout, this);
                Trace.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId} zk connect");

                //由于ZK客户端连接服务器是异步的，因此，此处阻塞线程，
                //防止连接还没有建立就调用ZK客户端导致异常；
                connectionResetEvent.WaitOne();
                connectionResetEvent.Dispose();
                connectionResetEvent = null;

                var stat = zk.existsAsync(rootNode, false).Sync();
                if (stat == null)
                {
                    // 创建根节点
                    zk.createAsync(rootNode, EmptyData, Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT).Sync();
                }
            }
            catch (KeeperException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// zookeeper节点的监视器
        /// </summary>
        public override Task process(WatchedEvent evt)
        {
            Trace.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId} State:[ {evt.getState()} ] , Type:[ {evt.get_Type()} ] , Path:[ {evt.getPath()} ]");

            var state = evt.getState();
            switch (state)
            {
                case Event.KeeperState.Disconnected:
                    break;
                case Event.KeeperState.SyncConnected:
                    //解除阻塞，此时可以调用ZK客户端；
                    connectionResetEvent?.Set();
                    break;
                case Event.KeeperState.AuthFailed:
                    break;
                case Event.KeeperState.ConnectedReadOnly:
                    break;
                case Event.KeeperState.Expired:
                    HandleExpired();
                    break;
                default:
                    break;
            }

            var eventType = evt.get_Type();
            switch (eventType)
            {
                case Event.EventType.None:
                    break;
                case Event.EventType.NodeCreated:
                    break;
                case Event.EventType.NodeDeleted:
                    break;
                case Event.EventType.NodeDataChanged:
                    break;
                case Event.EventType.NodeChildrenChanged:
                    break;
                default:
                    break;
            }

            //源代码此处没有校验删除的节点是否当前节点的前一节点，应该是有问题的；
            if (evt.getPath() == rootNode + "/" + preWaitNode &&
                evt.get_Type() == Event.EventType.NodeDeleted)
            {
                lockResetEvent?.Set();
            }

            return Task.CompletedTask;
        }

        private void HandleExpired()
        {
            try
            {
                zk?.closeAsync().Sync();
            }
            catch (KeeperException ex)
            {
                throw ex;
            }

            Init();
        }

        public virtual bool TryLock()
        {
            try
            {
                string splitStr = "_lock_";
                if (lockName.Contains(splitStr))
                {
                    //throw new LockException("lockName can not contains \\u000B");
                }

                //创建临时子节点
                currentNode = zk.createAsync(rootNode + "/" + lockName + splitStr, new byte[0], Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL_SEQUENTIAL).Sync();
                Trace.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId} " + currentNode + " is created ");
                //取出所有子节点
                IList<string> subNodes = zk.getChildrenAsync(rootNode, false).Sync().Children;
                //取出所有lockName的锁
                IList<string> lockObjNodes = subNodes.Where(c => c.StartsWith(lockName)).ToList().SortEx();

                Array alockObjNodes = lockObjNodes.ToArray();
                Array.Sort(alockObjNodes);
                Trace.WriteLine(currentNode + "==" + lockObjNodes[0]);
                if (currentNode.Equals(rootNode + "/" + lockObjNodes[0]))
                {
                    //如果是最小的节点,则表示取得锁
                    return true;
                }

                //如果不是最小的节点，找到比自己小1的节点
                string subMyZnode = currentNode.Substring(currentNode.LastIndexOf("/", StringComparison.Ordinal) + 1);
                preWaitNode = lockObjNodes[Array.BinarySearch(alockObjNodes, subMyZnode) - 1];

                Trace.WriteLine($"preWaitNode:{preWaitNode}");
            }
            catch (KeeperException e)
            {
                throw e;
            }

            return false;
        }

        public virtual bool TryLock(TimeSpan time)
        {
            return TryLock((int)time.TotalMilliseconds);
        }

        private bool WaitForLock(string lower, TimeSpan waitTime)
        {
            return WaitForLock(lower, (int)waitTime.TotalMilliseconds);
        }

        private bool WaitForLock(string lower, int? millisecondsTimeout)
        {
            var stat = zk.existsAsync(rootNode + "/" + lower, true).Sync();
            //判断比自己小一个数的节点是否存在,如果不存在则无需等待锁,同时注册监听
            if (stat != null)
            {
                Console.WriteLine("Thread " + Thread.CurrentThread.Name + " waiting for " + rootNode + "/" + lower);

                lockResetEvent = new AutoResetEvent(false);
                bool r = millisecondsTimeout.HasValue ? lockResetEvent.WaitOne(millisecondsTimeout.Value) : lockResetEvent.WaitOne();
                lockResetEvent.Dispose();
                lockResetEvent = null;
                return r;
            }
            else
            {
                return true;
            }
        }

        public void Dispose()
        {
            try
            {
                zk?.closeAsync().Sync();
            }
            catch (KeeperException e)
            {
                throw e;
            }
        }

        public bool Lock()
        {
            try
            {
                if (TryLock())
                {
                    return true;
                }

                return WaitForLock(preWaitNode, null);
            }
            catch (KeeperException e)
            {
                throw e;
            }
        }

        public bool TryLock(int millisecondsTimeout)
        {
            try
            {
                if (TryLock())
                {
                    return true;
                }

                return WaitForLock(preWaitNode, millisecondsTimeout);
            }
            catch (KeeperException e)
            {
                throw e;
            }
        }

        public void UnLock()
        {
            try
            {
                Trace.WriteLine("zk unlock " + currentNode);
                zk.deleteAsync(currentNode, AnyVersion).Sync();
                currentNode = null;
                preWaitNode = null;
            }
            catch (KeeperException e)
            {
                throw e;
            }
        }
    }
}
