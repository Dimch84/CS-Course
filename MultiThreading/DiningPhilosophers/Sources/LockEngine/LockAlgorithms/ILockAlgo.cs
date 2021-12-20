using System;

namespace DiningPhilosophers
{
    public interface ILockAlgo : IDisposable
    {
        bool TryLock(Int32[] lockIds);
    }

    public enum ELockAlgo
    {
        EBasic,
        ELockLevel,
        ETryWaiting
    }
}
