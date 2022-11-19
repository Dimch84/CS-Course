using System;
using System.Threading;

namespace Lectures
{
    internal sealed class MutexLocker : IDisposable
    {
        private readonly Mutex _mutex;

        public MutexLocker(Mutex mutex)
        {
            _mutex = mutex;
            _mutex.WaitOne();
        }

        public void Dispose()
        {
            _mutex.ReleaseMutex();
        }
    }
}
