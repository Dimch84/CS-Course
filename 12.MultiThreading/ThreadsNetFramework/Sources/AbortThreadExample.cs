using System;
using System.Threading;
using Common;

namespace ThreadsNetFramework
{
    internal class AbortThreadExample
    {
        static void ThreadNoReset()
        {
            CustomLogger.LogMessage("Thread1 start");
            try
            {
                Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
            }
            catch (Exception exc)
            {
               CustomLogger.LogMessage("Thread1: exc '{0}'", exc.Message);
                // Thread Abort перебрасывается дальше
            }
            // эта строка не будет выведена:
            CustomLogger.LogMessage("Thread1: end");
        }

        static void ThreadResetAbort()
        {
            CustomLogger.LogMessage("Thread2 start");
            try
            {
                Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread2: exc '{0}'", exc.Message);

                Thread.ResetAbort();
                CustomLogger.LogMessage("Thread2: ResetAbort");
            }
            CustomLogger.LogMessage("Thread2: end");
        }

        public void Run()
        {
            try
            {
                CustomLogger.LogStartExample("AbortThreadExample");

                Thread threadNoReset = new Thread(ThreadNoReset);
                threadNoReset.Start();
                Thread.Sleep(CustomTimeouts.ShortTimePeriod);
                threadNoReset.Abort();
                threadNoReset.Join();

                Thread threadReset = new Thread(ThreadResetAbort);
                threadReset.Start();
                Thread.Sleep(CustomTimeouts.ShortTimePeriod);
                threadReset.Abort();
                threadReset.Join();
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
