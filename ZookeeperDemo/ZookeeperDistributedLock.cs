using System;
using System.Diagnostics;
using System.Threading;
using org.apache.zookeeper;
using static org.apache.zookeeper.ZooDefs;
using System.Linq;
using System.Threading.Tasks;

namespace ZookeeperDemo
{
    public class ZookeeperDistributedLock : Watcher, IDistributedLock
    {

        private readonly ZooKeeper zk;
        private readonly string zooKeeperConnectstring = "127.0.0.1:2181";

        private readonly string lockRoot = "/lock";

        private bool isGetLock = false;

        private bool preLockNodeDelete = false;

        private string currentLockNode;

        private static readonly byte[] EmptyData = new byte[0];

        public ZookeeperDistributedLock()
        {
            zk = new ZooKeeper(zooKeeperConnectstring, 60 * 1000, NullWatcher.Instance);

            var stat = zk.existsAsync(lockRoot, true).Run();
            if (stat == null)
            {
                zk.createAsync(lockRoot, EmptyData, Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT).Run();
            }
        }

        public void Dispose()
        {
            zk?.closeAsync().Run();
        }

        public void Lock()
        {
            currentLockNode = zk.createAsync($"{lockRoot}/children", "children".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL_SEQUENTIAL).Run();

            var children = zk.getChildrenAsync(lockRoot, true).Run().Children.ToList().SortEx();
            var firstOne = children.FirstOrDefault();
            if (currentLockNode == firstOne)
            {
                isGetLock = true;
                return;
            }
            else
            {
                //如果不是则表示锁已经被其它客户端获取，因此客户端A要等待它释放锁，也就是等待获取到锁的那个客户端B把自己创建的那个节点删除。
                //再次与自己创建的node_n节点对比，直到自己创建的node_n是locker的所有子节点中顺序号最小的，此时表示客户端A获取到了锁！
                //此时就通过监听比node_n次小的那个顺序节点的删除事件来知道客户端B是否已经释放了锁，如果是，此时客户端A再次获取locker下的所有子节点，
                var index = children.IndexOf(currentLockNode);
                var preLockNode = children[index - 1];

                var data = zk.getDataAsync(preLockNode, this);

            }
        }

        public void UnLock()
        {
            throw new NotImplementedException();
        }

        public void TryLock()
        {
            throw new NotImplementedException();
        }

        public void TryLock(int millisecondsTimeout)
        {
            throw new NotImplementedException();
        }

        public void TryLock(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public override Task process(WatchedEvent @event)
        {
            throw new NotImplementedException();
        }

        #region private

        string ProcessName { get { return Process.GetCurrentProcess().ProcessName; } }
        int ProcessID { get { return Process.GetCurrentProcess().Id; } }
        int ThreadID { get { return Thread.CurrentThread.ManagedThreadId; } }


        #endregion
    }
}
