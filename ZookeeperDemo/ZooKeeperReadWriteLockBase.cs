using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using org.apache.zookeeper;

namespace ZookeeperDemo
{
    public abstract class ZooKeeperReadWriteLockBase : ZooKeeperLockBase, IDistributedLock
    {
        protected string currentNode;
        protected string watchNode;

        protected AutoResetEvent lockResetEvent;
        protected AutoResetEvent connectionResetEvent;

        public ZooKeeperReadWriteLockOptions Options { get; protected set; }

        protected override void Init()
        {
            connectionResetEvent = new AutoResetEvent(false);

            var watch = new Stopwatch();
            watch.Start();
            SafeLog($"Try to connect ZooKeeper [ {Options.ZkConfig} ].");

            zk = new ZooKeeper(Options.ZkConfig, Options.SessionTimeout, this);

            //由于ZK客户端连接服务器是异步的，因此，此处阻塞线程，
            //防止连接还没有建立就调用ZK客户端导致异常；
            connectionResetEvent.WaitOne();
            connectionResetEvent.Close();
            connectionResetEvent = null;

            watch.Stop();

            SafeLog($"ZooKeeper connected cost [ {watch.Elapsed.TotalSeconds.ToString("0.000")} ] s.");

            CreateRootNodeIfNotExist(Options.ReadLockRootNodeName);
            CreateRootNodeIfNotExist(Options.WriteLockRootNodeName);
        }

        public bool Lock()
        {
            return InnerLock(null);
        }

        public void UnLock()
        {
            zk.deleteAsync(currentNode, ZooKeeperDefaults.AnyVersion).Sync();

            SafeLog($"Unlock,delete node [ {currentNode} ].");
        }

        public bool TryLock()
        {
            return InnerTryLock();
        }

        public bool TryLock(TimeSpan timeout)
        {
            return InnerLock((int)timeout.TotalMilliseconds);
        }

        public bool TryLock(int millisecondsTimeout)
        {
            return InnerLock(millisecondsTimeout);
        }

        protected virtual bool InnerLock(int? millisecondsTimeout)
        {
            if (InnerTryLock())
            {
                return true;
            }

            return WaitForLock(millisecondsTimeout);
        }

        protected virtual bool WaitForLock(int? millisecondsTimeout)
        {
            var watchNodePath = Options.WriteLockRootNodeName + "/" + watchNode;
            var stat = zk.existsAsync(watchNodePath, true).Sync();
            if (stat == null)
            {
                SafeLog($"Watch node [ {watchNodePath} ] dose not exist, get the lock.");

                return true;
            }


            //TODO：疑问：在existsAsync调用后，是否一定能够收到删除节点事件？

            var watch = new Stopwatch();
            watch.Start();

            lockResetEvent = new AutoResetEvent(false);
            bool success = millisecondsTimeout.HasValue ? lockResetEvent.WaitOne(millisecondsTimeout.Value) : lockResetEvent.WaitOne();
            lockResetEvent.Close();

            watch.Stop();

            if (millisecondsTimeout.HasValue)
            {
                SafeLog($"Wait {watch.Elapsed.TotalSeconds.ToString("0.000")} s  Get lock [ {(success ? "success" : "fail")} ].");
            }
            else
            {
                SafeLog($"Get lock [ {(success ? "success" : "fail")} ], wait [ {watch.Elapsed.TotalSeconds.ToString("0.000")} ] s.");
            }

            return success;
        }

        public override void Dispose()
        {
            lockResetEvent?.Close();
            connectionResetEvent?.Close();

            base.Dispose();
        }

        public override Task process(WatchedEvent evt)
        {
            SafeLog($"WatchedEvent State:[ {evt.getState()} ] , Type:[ {evt.get_Type()} ] , Path:[ {evt.getPath()} ]");

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

            if (IsWatchedNodeDeleted(evt))
            {
                SafeLog($"Watch node [ {evt.getPath()} ] deleted.");

                HandleWatchedNodeDeleted();
            }

            return Task.CompletedTask;
        }

        protected virtual bool IsWatchedNodeDeleted(WatchedEvent evt)
        {
            return evt.getPath() == Options.WriteLockRootNodeName + "/" + watchNode &&
                evt.get_Type() == Event.EventType.NodeDeleted;
        }

        protected virtual void HandleWatchedNodeDeleted()
        {
            lockResetEvent?.Set();
        }

        protected abstract bool InnerTryLock();
    }
}
