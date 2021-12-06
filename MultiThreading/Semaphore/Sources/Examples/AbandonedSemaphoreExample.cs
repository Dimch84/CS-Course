using System;
using System.Threading;
using Common;

namespace Lectures
{
    class AbandonedSemaphoreExample : IDisposable
    {
        private readonly Semaphore _semaphore;

        public AbandonedSemaphoreExample()
        {
            _semaphore = new Semaphore(initialCount: 1, maximumCount: 1);
        }

        public void Dispose()
        {
            _semaphore.Dispose();
        }

        void Thread1()
        {
            try
            {
                CustomLogger.LogMessage("Thread1: '{0}' started", Thread.CurrentThread.ManagedThreadId);

                _semaphore.WaitOne();
                CustomLogger.LogMessage("Thread1: mutex acquired");

                CustomLogger.LogMessage("Thread1: '{0}' finished", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread2: '{0}'", exc.Message);
            }
        }

        void Thread2()
        {
            try
            {
                CustomLogger.LogMessage("Thread2: '{0}' started", Thread.CurrentThread.ManagedThreadId);

                Thread.Sleep(TimeSpan.FromSeconds(2));

                CustomLogger.LogMessage("Thread2: trying to acquire semaphore");
                _semaphore.WaitOne(); // hangs here
                CustomLogger.LogMessage("Thread2: semaphore acquired");

                CustomLogger.LogMessage("Thread2: '{0}' finished", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread2: '{0}'", exc.Message);
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("AbandonedSemaphoreExample");
            Thread thread1 = new Thread(Thread1);
            thread1.Start();

            Thread thread2 = new Thread(Thread2);
            thread2.Start();

            thread1.Join();
            thread2.Join();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
