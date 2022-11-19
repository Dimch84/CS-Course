using System.Threading;

namespace ReadWriteLock
{
    sealed class ReadWriteLockSlimWrapper : IReadWriteLock
    {
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public ReadWriteLockSlimWrapper()
        {

        }

        public void Dispose()
        {
            _rwLock.Dispose();
        }

        public void EnterReadLock()
        {
            _rwLock.EnterReadLock();
        }

        public void ExitReadLock()
        {
            _rwLock.ExitReadLock();
        }

        public void EnterWriteLock()
        {
            _rwLock.EnterWriteLock();
        }

        public void ExitWriteLock()
        {
            _rwLock.ExitWriteLock();
        }
    }
}
