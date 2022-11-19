using System;
using System.Collections.Generic;
using Common;

namespace DiningPhilosophers
{
    internal sealed class LockTryWaitingAlgo : ILockAlgo
    {
        private readonly LockContainer _lockContainer;
        private readonly String _philName;
        private List<Int32> _acquiredLockIds = new List<int>();

        public LockTryWaitingAlgo(LockContainer lockContainer, String philName)
        {
            _lockContainer = lockContainer;
            _philName = philName;
        }

        public bool TryLock(Int32[] lockIds)
        {
            for (int i = 0; i < lockIds.Length; ++i)
            {
                CustomLogger.LogMessage("{0} trying to get {1}", _philName, lockIds[i]);
                if (!_lockContainer.TryLock(lockIds[i]))
                {
                    CustomLogger.LogMessage("===={0} unable to get {1}", _philName, lockIds[i]);
                    return false;
                }

                _acquiredLockIds.Add(lockIds[i]);
                CustomLogger.LogMessage("{0} get {1}", _philName, lockIds[i]);
            }

            return true;
        }

        public void Dispose()
        {
            for (int i = _acquiredLockIds.Count - 1; i >= 0; --i)
            {
                _lockContainer.Unlock(_acquiredLockIds[i]);
                CustomLogger.LogMessage("{0} put fork {1}", _philName, _acquiredLockIds[i]);
            }
        }
    }
}
