using System;
using System.Threading;
using Common;

namespace Lectures
{
    //If the named system semaphore does not exist, it is created with the initial count and maximum count specified by initialCount and maximumCount. 
    //If the named system semaphore already exists, initialCount and maximumCount are not used, although invalid values still cause exceptions.

    internal class NamedSemaphoreExample
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

                    semaphore.WaitOne();

                    CustomLogger.LogMessage("Thread1: '{0}' semaphore acquired", Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(TimeSpan.FromSeconds(5));

                    semaphore.Release();

                    CustomLogger.LogMessage("Thread1: '{0}' semaphore released", Thread.CurrentThread.ManagedThreadId);
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogException(e);
            }

            CustomLogger.LogMessage("Thread1: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        private void Thread2()
        {
            CustomLogger.LogMessage("Thread2: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            try
            {
                //using (Semaphore semaphore = new Semaphore(0, 1, _semaphoreName, out var createdNew))
                using (Semaphore semaphore = new Semaphore(2, 2, _semaphoreName, out var createdNew))
                {

                    CustomLogger.LogMessage("Thread2: '{0}', semaphore created '{1}'", Thread.CurrentThread.ManagedThreadId, createdNew);

                    semaphore.WaitOne();

                    CustomLogger.LogMessage("Thread2: '{0}' semaphore acquired", Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(TimeSpan.FromSeconds(5));

                    semaphore.Release();

                    CustomLogger.LogMessage("Thread2: '{0}' semaphore released", Thread.CurrentThread.ManagedThreadId);
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogException(e);
            }

            CustomLogger.LogMessage("Thread2: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("NamedSemaphoreExample");

            try
            {
                Thread thread1 = new Thread(Thread1);
                thread1.Start();

                Thread.Sleep(TimeSpan.FromSeconds(1));

                Thread thread2 = new Thread(Thread2);
                thread2.Start();

                thread1.Join();
                thread2.Join();
            }
            catch (Exception e)
            {
                CustomLogger.LogException(e);
            }
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
