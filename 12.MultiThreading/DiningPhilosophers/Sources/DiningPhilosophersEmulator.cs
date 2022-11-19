using System;
using System.Threading.Tasks;
using Common;

namespace DiningPhilosophers
{
    class DiningPhilosophersEmulator
    {
        private readonly Int32 _philCount;
        private readonly LockContainer _lockContainer;
        private readonly ELockAlgo _lockAlgo;

        public DiningPhilosophersEmulator(Int32 philCount, ELockAlgo lockAlgo)
        {
            _philCount = philCount;
            _lockContainer = new LockContainer(_philCount);
            _lockAlgo = lockAlgo;
        }

        private void Philosopher(Int32 philosopherNumber)
        {
            Int32[] forks = GetForksNumber(philosopherNumber);
            while (true)
            {
                using (ILockAlgo lockAlgo = LockFactory.CreateLockAlgo(philosopherNumber.ToString(), _lockContainer, _lockAlgo))
                {
                    if (lockAlgo.TryLock(forks))
                    {
                        CustomLogger.LogMessage("+++++++++++++{0} eating", philosopherNumber);
                    }
                }
            }
        }

        private Int32[] GetForksNumber(Int32 philNumber)
        {
            Int32 leftFork = philNumber;
            Int32 rightFork = (philNumber == _philCount - 1)
                ? 0
                : philNumber + 1;
            Int32[] forks = new Int32[] { leftFork, rightFork };
            return forks;
        }

        public void Run()
        {
            Task[] tasks = new Task[_philCount];
            for (int i = 0; i < tasks.Length; ++i)
            {
                Int32 temp = i;
                tasks[i] = Task.Run(() => Philosopher(temp));
            }

            Task.WaitAll(tasks);
        }
    }
}
