using System;
using System.Threading;
using Common;

namespace Events.Sources
{
    internal sealed class CountDownEventExample : IDisposable
    {
        private const Int32 TestThreadNumber = 5;
        private readonly CountdownEvent _countDownEvent = new CountdownEvent(TestThreadNumber);

        public void Dispose()
        {
            _countDownEvent.Dispose();
        }

        public void ThreadRoutine(Int32 threadNumber)
        {
            CustomLogger.LogMessage("Thread#{0} started", threadNumber);

            Thread.Sleep(TimeSpan.FromSeconds(threadNumber));
            _countDownEvent.Signal();
            CustomLogger.LogMessage("Thread#{0} signal event", threadNumber);

            CustomLogger.LogMessage("Thread#{0} ended", threadNumber);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("CountDownEvent example");

            try
            {                                
                Thread[] threads = new Thread[TestThreadNumber];

                for (Int32 i = 0; i < TestThreadNumber; ++i)
                {
                    Int32 copy = i;
                    threads[i] = new Thread(() => ThreadRoutine(copy));
                    threads[i].Start();
                }

                CustomLogger.LogMessage("Main Thread: waiting for event");
                _countDownEvent.Wait();
                CustomLogger.LogMessage("Main Thread: countdownEvent signaled");

                for (Int32 i = 0; i < TestThreadNumber; ++i)
                {
                    threads[i].Join();
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
            }

            CustomLogger.LogEndExample();
            Console.ReadLine();
        }
    }
}
