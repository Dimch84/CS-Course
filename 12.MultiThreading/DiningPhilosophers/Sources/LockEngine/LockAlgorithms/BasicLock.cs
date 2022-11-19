using System;
using System.Collections.Generic;
using Common;

namespace DiningPhilosophers
{
    internal sealed class BasicLockAlgo: ILockAlgo
    {
        private readonly LockContainer _lockContainer;
        private readonly String _philName;
        private readonly List<Int32> _acquiredLockIds = new List<int>();

        public BasicLockAlgo(LockContainer lockContainer, String philName)
        {
            _lockContainer = lockContainer;
            _philName = philName;
        }

        public bool TryLock(Int32[] lockIds)
        {
            for (int i = 0; i < lockIds.Length; ++i)
            {
                CustomLogger.LogMessage("{0} trying to get {1}", _philName, lockIds[i]);
                _lockContainer.Lock(lockIds[i]);
                _acquiredLockIds.Add(lockIds[i]);
                CustomLogger.LogMessage("{0} get {1}", _philName, lockIds[i]);
            }

            return true;
        }

        public void Dispose()
        {
            for (int i = _acquiredLockIds.Count-1; i >= 0; --i )
            {
                _lockContainer.Unlock(_acquiredLockIds[i]);
                CustomLogger.LogMessage("{0} put fork {1}", _philName, _acquiredLockIds[i]);
            }
        }
    }
}
