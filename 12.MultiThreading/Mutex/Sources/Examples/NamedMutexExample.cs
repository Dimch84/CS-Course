using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class NamedMutexExample
    {
        private void Thread1()
        {
            CustomLogger.LogMessage("Thread1: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            using (Mutex mutex = new Mutex(false, "NamedMutexExample", out var createdNew))
            {

                CustomLogger.LogMessage("Thread1: '{0}', mutex created '{1}'", Thread.CurrentThread.ManagedThreadId, createdNew);

                mutex.WaitOne();

                CustomLogger.LogMessage("Thread1: '{0}' mutex acquired", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

                mutex.ReleaseMutex();

                CustomLogger.LogMessage("Thread1: '{0}' mutex released", Thread.CurrentThread.ManagedThreadId);
            }

            CustomLogger.LogMessage("Thread1: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        private void Thread2()
        {
            CustomLogger.LogMessage("Thread2: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            using (Mutex mutex = new Mutex(false, "NamedMutexExample", out var createdNew))
            {

                CustomLogger.LogMessage("Thread2: '{0}', mutex created '{1}'", Thread.CurrentThread.ManagedThreadId, createdNew);

                mutex.WaitOne();

                CustomLogger.LogMessage("Thread2: '{0}' mutex acquired", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

                mutex.ReleaseMutex();

                CustomLogger.LogMessage("Thread2: '{0}' mutex released", Thread.CurrentThread.ManagedThreadId);
            }

            CustomLogger.LogMessage("Thread2: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }


        private void Thread3()
        {
            CustomLogger.LogMessage("Thread3: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            using (Mutex mutex = new Mutex(false, "NamedMutexExample", out var createdNew))
            {

                CustomLogger.LogMessage("Thread3: '{0}', mutex created '{1}'", Thread.CurrentThread.ManagedThreadId, createdNew);

                mutex.WaitOne();

                CustomLogger.LogMessage("Thread3: '{0}' mutex acquired", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

                mutex.ReleaseMutex();

                CustomLogger.LogMessage("Thread3: '{0}' mutex released", Thread.CurrentThread.ManagedThreadId);
            }

            CustomLogger.LogMessage("Thread3: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("NamedMutexExample");
            Thread thread1 = new Thread(Thread1);
            thread1.Start();

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            Thread thread2 = new Thread(Thread2);
            thread2.Start();

            thread1.Join();

            Thread thread3 = new Thread(Thread3);
            thread3.Start();

            thread2.Join();
            thread3.Join();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
