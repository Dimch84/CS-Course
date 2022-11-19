using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class AbandonedMutexExample : IDisposable
    {
        private readonly Mutex _mutex;

        public AbandonedMutexExample()
        {
            _mutex = new Mutex(false);
        }

        public void Dispose()
        {
            _mutex.Dispose();
        }

        void Thread1()
        {
            try
            {
                CustomLogger.LogMessage("Thread1: '{0}' started", Thread.CurrentThread.ManagedThreadId);

                _mutex.WaitOne();
                CustomLogger.LogMessage("Thread1: mutex acquired");

                CustomLogger.LogMessage("Thread1: '{0}' finished", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread1: '{0}'", exc.Message);
            }
        }

        void Thread2()
        {
            try
            {
                CustomLogger.LogMessage("Thread2: '{0}' started", Thread.CurrentThread.ManagedThreadId);

                Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

                CustomLogger.LogMessage("Thread2: trying to acquire mutex");
                _mutex.WaitOne();
                CustomLogger.LogMessage("Thread2: mutex acquired");

                CustomLogger.LogMessage("Thread2: '{0}' finished", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread2: '{0}'", exc.Message);
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("AbandonedMutexExample");
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
