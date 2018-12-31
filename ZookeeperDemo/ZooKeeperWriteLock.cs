using System;
using org.apache.zookeeper;
using static org.apache.zookeeper.ZooDefs;

namespace ZookeeperDemo
{
    /// <summary>
    /// 基于Zookeeper的读写锁
    /// 参见：http://blog.yuanxiaolong.cn/blog/2015/01/13/zookeeper-distribute-read-write-lock/
    /// </summary>
    public class ZooKeeperWriteLock : ZooKeeperReadWriteLockBase
    {
        public ZooKeeperWriteLock(ZooKeeperReadWriteLockOptions options)
        {
            Options = options;

            Init();
        }

        protected override bool InnerTryLock()
        {
            currentNode = zk.createAsync(Options.WriteLockRootNodeName + "/" + Options.WriteLockName, ZooKeeperDefaults.EmptyData, Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL_SEQUENTIAL).Sync();

            SafeLog($"Node [ {currentNode} ] is created.");

            var writeNodes = zk.getChildrenAsync(Options.WriteLockRootNodeName, false).Sync().Children.SortEx();

            if (currentNode == Options.WriteLockRootNodeName + "/" + writeNodes[0])
            {
                SafeLog($"There is no write lock wait here, got the read lock.");

                return true;
            }

            Array alockObjNodes = writeNodes.ToArray();
            Array.Sort(alockObjNodes);

            //如果不是最小的节点，找到比自己小1的节点
            string subMyZnode = currentNode.Substring(currentNode.LastIndexOf("/", StringComparison.Ordinal) + 1);
            watchNode = writeNodes[Array.BinarySearch(alockObjNodes, subMyZnode) - 1];
            SafeLog($"There is write lock, and the previous lock is [ {Options.WriteLockRootNodeName + "/" + watchNode} ].");

            return false;
        }
    }
}
