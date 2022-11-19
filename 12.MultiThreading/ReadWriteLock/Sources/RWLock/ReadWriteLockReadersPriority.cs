using System;
using System.Threading;

namespace ReadWriteLock
{
    internal sealed class ReadWriteLockReadersPriority : IReadWriteLock
    {
        private readonly SemaphoreSlim _resourceLock = new SemaphoreSlim(1, 1);
        private readonly Object _readersLock = new object();
        private Int32 _readersCount;

        public ReadWriteLockReadersPriority()
        {

        }

        public void Dispose()
        {

        }

        public void EnterReadLock()
        {
            lock (_readersLock)
            {
                ++_readersCount;

                if (_readersCount == 1)
                    _resourceLock.Wait();
            }
        }

        public void ExitReadLock()
        {
            lock (_readersLock)
            {
                --_readersCount;

                if (_readersCount == 0)
                    _resourceLock.Release();
            }
        }

        public void EnterWriteLock()
        {
            _resourceLock.Wait();
        }

        public void ExitWriteLock()
        {
            _resourceLock.Release();
        }
    }
}
