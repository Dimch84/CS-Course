using System;

namespace DiningPhilosophers
{
    public static class LockFactory
    {
        public static ILockAlgo CreateLockAlgo(String philName, LockContainer lockContainer, ELockAlgo lockAlgo)
        {
            switch(lockAlgo)
            {
                case ELockAlgo.EBasic:
                    return new BasicLockAlgo(lockContainer, philName);
                case ELockAlgo.ELockLevel:
                    return new LockLevelAlgo(lockContainer, philName);
                case ELockAlgo.ETryWaiting:
                    return new LockTryWaitingAlgo(lockContainer, philName);
                default:
                    throw new Exception("Unsupported lock level");
            }
        }
    }
}
