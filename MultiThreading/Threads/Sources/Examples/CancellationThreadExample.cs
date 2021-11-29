using Common;
using System;
using System.Threading;

namespace Threads
{
    internal class CancellationThreadExample
    {        
        static void ThreadCancellation(object obj)
        {
            CustomLogger.LogMessage("Thread start");
            CancellationToken token = (CancellationToken)obj;
            try
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    CustomLogger.LogMessage("Thread: do work");
                    Thread.Sleep(CustomTimeouts.ShortTimePeriod);
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread: exc '{0}'", exc.Message);
            }
            CustomLogger.LogMessage("Thread: end");
        }

        public void Run()
        {
            try
            {
                CustomLogger.LogStartExample("CancellationThreadExample");

                using (CancellationTokenSource cancellationSource = new CancellationTokenSource())
                {
                    Thread thread = new Thread(ThreadCancellation);
                    thread.Start(cancellationSource.Token);
                    Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
                    cancellationSource.Cancel();
                    thread.Join();
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
