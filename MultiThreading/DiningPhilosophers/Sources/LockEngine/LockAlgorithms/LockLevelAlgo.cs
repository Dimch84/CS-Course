using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace DiningPhilosophers
{
    sealed class LockLevelAlgo : ILockAlgo
    {
        private readonly LockContainer _lockContainer;
        private readonly String _philName;
        private List<Int32> _acquiredLockIds = new List<int>();

        public LockLevelAlgo(LockContainer lockContainer, String philName)
        {
            _lockContainer = lockContainer;
            _philName = philName;
        }

        public bool TryLock(Int32[] lockIds)
        {
            Int32[] lockIdsOrdered = lockIds.OrderBy(lockId => lockId).ToArray();

            for (int i = 0; i < lockIdsOrdered.Length; ++i)
            {
                CustomLogger.LogMessage("{0} trying to get {1}", _philName, lockIdsOrdered[i]);
                _lockContainer.Lock(lockIdsOrdered[i]);
                _acquiredLockIds.Add(lockIdsOrdered[i]);
                CustomLogger.LogMessage("{0} get {1}", _philName, lockIdsOrdered[i]);
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
