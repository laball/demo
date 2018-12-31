using System;

namespace ZookeeperDemo
{
    interface IDistributedLock : IDisposable
    {
        bool Lock();
        bool TryLock();
        bool TryLock(int millisecondsTimeout);
        bool TryLock(TimeSpan timeout);
        void UnLock();
    }
}
