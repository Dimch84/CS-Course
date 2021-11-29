using System;
using System.Threading;
using Common;

namespace Threads
{
    internal class AbortThreadExample
    {
        static void Thread()
        {
            CustomLogger.LogMessage("Thread start");
            try
            {
                System.Threading.Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
            }
            catch (Exception exc)
            {
               CustomLogger.LogMessage("Thread: exc '{0}'", exc.Message);                
            }
            CustomLogger.LogMessage("Thread: end");
        }
      
        public void Run()
        {
            CustomLogger.LogStartExample("AbortThreadExample");

            Thread thread = new Thread(Thread);
            thread.Start();
            System.Threading.Thread.Sleep(CustomTimeouts.ShortTimePeriod);
            try
            {                
                thread.Abort();                
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
            }
            thread.Join();

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
