using Common;
using System;
using System.Threading;

namespace Lectures
{
    internal sealed class ThreadPoolDeadLockExample
    {
        private const Int32 WorkItemsCount = 10;
        private const Int32 MaxWorkerThreadsCount = 8;

        public void Run()
        {
            CustomLogger.LogStartExample("thread pool deadlock example");
            Int32 workerThreads;
            Int32 completionPortThreads;
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            CustomLogger.LogMessage("Max Worker thread '{0}', max completion port thread '{1}'", workerThreads, completionPortThreads);
            ThreadPool.SetMaxThreads(MaxWorkerThreadsCount, completionPortThreads);
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            CustomLogger.LogMessage("Update: Max Worker thread '{0}', max completion port thread '{1}'", workerThreads, completionPortThreads);

            ManualResetEvent outerEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(delegate
            {
                ManualResetEvent innerEvent = new ManualResetEvent(false);
                for (int i = 0; i<WorkItemsCount; ++i)
                {
                    ThreadPool.QueueUserWorkItem(delegate (object state)
                    {
                        int index = (int)state;
                        CustomLogger.LogMessage("{0} running...", index);

                        if (index == WorkItemsCount-1)
                        {
                            CustomLogger.LogMessage("Settings inner event");
                            innerEvent.Set();                            
                        }
                        else
                        {
                            innerEvent.WaitOne();
                        }
                    }, i);
                }

                innerEvent.WaitOne();
                outerEvent.Set();
            });

            CustomLogger.LogMessage("Main thread: waiting");
            outerEvent.WaitOne();           

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
