using System.Diagnostics;
using System.Threading;
using org.apache.zookeeper;
using static org.apache.zookeeper.ZooDefs;

namespace ZookeeperDemo
{
    /// <summary>
    /// 基于Zookeeper的读写锁
    /// 参见：http://blog.yuanxiaolong.cn/blog/2015/01/13/zookeeper-distribute-read-write-lock/
    /// </summary>
    public class ZooKeeperReadLock : ZooKeeperReadWriteLockBase
    {
        public ZooKeeperReadLock(ZooKeeperReadWriteLockOptions options)
        {
            Options = options;

            Init();
        }

        #region Read Lock

        protected override bool InnerTryLock()
        {
            var nodePath = Options.ReadLockRootNodeName + "/" + Options.ReadLockName;
            currentNode = zk.createAsync(nodePath, ZooKeeperDefaults.EmptyData, Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL_SEQUENTIAL).Sync();

            SafeLog($"Node [ {currentNode} ] is created.");

            var writeNodes = zk.getChildrenAsync(Options.WriteLockRootNodeName, false).Sync().Children.SortEx();
            if (writeNodes.Count == 0)
            {
                SafeLog($"There is no write lock, got the read lock.");

                return true;
            }
            else
            {
                watchNode = writeNodes[writeNodes.Count - 1];
                SafeLog($"There is write lock, and the previous lock is [ {Options.WriteLockRootNodeName + "/" + watchNode} ].");

                return false;
            }
        }

        #endregion

    }
}
