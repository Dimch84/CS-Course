using System;

namespace ReadWriteLock
{
    internal interface IReadWriteLock : IDisposable
    {
        void EnterReadLock();
        void ExitReadLock();

        void EnterWriteLock();
        void ExitWriteLock();
    }
}
