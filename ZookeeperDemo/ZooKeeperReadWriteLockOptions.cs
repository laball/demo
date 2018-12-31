namespace ZookeeperDemo
{
    public class ZooKeeperReadWriteLockOptions
    {
        public string ZkConfig { get; set; } = "127.0.0.1:2181";
        public string ReadLockRootNodeName { get; set; } = "/lock/read";
        public string ReadLockName { get; set; } = "read_";
        public string WriteLockRootNodeName { get; set; } = "/lock/write";
        public string WriteLockName { get; set; } = "write_";

        public int SessionTimeout { get; set; } = ZooKeeperDefaults.SessionTimeout;
    }
}
