using System;
using System.Threading;

namespace ReadWriteLock
{
    sealed class ReadWriteLockWritersPriority : IReadWriteLock
    {
        private readonly SemaphoreSlim _resourceLock = new SemaphoreSlim(1, 1);
        private readonly Object _readersLock = new object();
        private readonly Object _writersLock = new object();
        private readonly SemaphoreSlim _tryEnterReaderLock = new SemaphoreSlim(1, 1);
        private Int32 _readersCount;
        private Int32 _writersCount;

        public ReadWriteLockWritersPriority()
        {

        }

        public void Dispose()
        {

        }

        public void EnterReadLock()
        {
            _tryEnterReaderLock.Wait();
            {
                lock (_readersLock)
                {
                    ++_readersCount;

                    if (_readersCount == 1)
                        _resourceLock.Wait();
                }
            }
            _tryEnterReaderLock.Release();
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
            lock (_writersLock)
            {
                ++_writersCount;

                if (_writersCount == 1)
                    _tryEnterReaderLock.Wait();
            }

            _resourceLock.Wait();
        }

        public void ExitWriteLock()
        {
            _resourceLock.Release();

            lock (_writersLock)
            {
                --_writersCount;

                if (_writersCount == 0)
                    _tryEnterReaderLock.Release();
            }
        }

    }
}
