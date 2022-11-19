using System;
using System.Threading;

namespace DiningPhilosophers
{
    public sealed class LockContainer
    {
        private readonly Object[] _locks;

        public LockContainer(Int32 lockCount)
        {
            _locks = new object[lockCount];
            for (int i = 0; i < _locks.Length; ++i)
            {
                _locks[i] = new object();
            }
        }

        public void Lock(Int32 lockId)
        {
            Monitor.Enter(_locks[lockId]);
        }

        public bool TryLock(Int32 lockId)
        {
            return Monitor.TryEnter(_locks[lockId]);
        }

        public void Unlock(Int32 lockId)
        {
            Monitor.Exit(_locks[lockId]);
        }
    }
}
