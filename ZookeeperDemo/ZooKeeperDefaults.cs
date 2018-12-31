using System;

namespace ZookeeperDemo
{
    public static class ZooKeeperDefaults
    {
        public const int AnyVersion = -1;
        public const int SessionTimeout = 30 * 1000;

        public static readonly byte[] EmptyData = Array.Empty<byte>();
    }
}