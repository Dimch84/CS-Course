using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class NamedSemaphoreWithMutexExample
    {
        private const String _semaphoreName = "NamedSemaphoreExample";

        private void Thread1()
        {
            CustomLogger.LogMessage("Thread1: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            try
            {
                using (Semaphore semaphore = new Semaphore(1, 1, _semaphoreName, out var createdNew))
                {

                    CustomLogger.LogMessage("Thread1: '{0}', semaphore created '{1}'", Thread.CurrentThread.ManagedThreadId, createdNew);

                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage(exc.Message);
            }

            CustomLogger.LogMessage("Thread1: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        private void Thread2()
        {
            CustomLogger.LogMessage("Thread2: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            try
            {
                using (Mutex mutex = new Mutex(false, _semaphoreName, out var createdNew))
                {

                    CustomLogger.LogMessage("Thread2: '{0}', mutex created '{1}'", Thread.CurrentThread.ManagedThreadId, createdNew);
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage(exc.Message);
            }

            CustomLogger.LogMessage("Thread2: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("NamedSemaphoreExample2");
            Thread thread1 = new Thread(Thread1);
            thread1.Start();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            Thread thread2 = new Thread(Thread2);
            thread2.Start();

            thread1.Join();
            thread2.Join();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
