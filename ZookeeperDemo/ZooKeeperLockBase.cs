using System;
using System.Diagnostics;
using System.Threading;
using org.apache.zookeeper;
using static org.apache.zookeeper.ZooDefs;

namespace ZookeeperDemo
{
    public abstract class ZooKeeperLockBase : Watcher, IDisposable
    {
        protected ZooKeeper zk;

        public Action<string> Log { get; set; } = message => Trace.WriteLine($"Thread: [ {Thread.CurrentThread.Name ?? Thread.CurrentThread.ManagedThreadId.ToString()} ] " + message);

        protected virtual void CreateRootNodeIfNotExist(string nodeName)
        {
            var stat = zk.existsAsync(nodeName, false).Sync();
            if (stat == null)
            {
                var nodePath = zk.createAsync(nodeName, ZooKeeperDefaults.EmptyData, Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT).Sync();
                SafeLog($"create znode {nodePath}");
            }
        }

        protected virtual void HandleExpired()
        {
            SafeLog("Session expired.");

            zk?.closeAsync().Sync();

            Init();
        }

        protected virtual void SafeLog(string message)
        {
            Log?.Invoke(message);
        }

        public virtual void Dispose()
        {
            zk?.closeAsync().Sync();
        }

        protected abstract void Init();
    }
}
