using Common;
using System;
using System.Threading;

namespace Threads
{
    internal class ThreadStartOrderExample
    {
        private void ThreadRoutine(Int32 threadNumber)
        {
            CustomLogger.LogMessage("Thread.LocalNumber = {0}: start", threadNumber);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("StartOrderExample");

            const Int32 TestThreadNumber = 10;
            Thread[] threads = new Thread[TestThreadNumber];

            for (Int32 i = 0; i < TestThreadNumber; ++i)
            {
                Int32 copy = i;
                threads[i] = new Thread(() => ThreadRoutine(copy));
            }

            for (Int32 i = 0; i < TestThreadNumber; ++i)
            {
                threads[i].Start();
            }

            for (Int32 i = 0; i < TestThreadNumber; ++i)
            {
                threads[i].Join();
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
