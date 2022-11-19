using System;
using System.Threading;
using Common;

namespace Timers
{    
    internal sealed class ExecuteOneByOneThreadPoolTimerExampleByLock
    {        
        private readonly Object _timerCallbackLock = new Object();        

        public ExecuteOneByOneThreadPoolTimerExampleByLock()
        {                        
        }      

        private readonly TimeSpan _timerInternal = TimeSpan.FromSeconds(2);
        private void TimerCallback(object state)
        {
            if (Monitor.TryEnter(_timerCallbackLock))
            {
                try
                {
                    TimerCallbackState timerCallbackState = (TimerCallbackState)state;
                    Int32 callbackNumber = timerCallbackState.GetNextCallbackNumber();
                    CustomLogger.LogMessage("Timer callback started: '{0}', IsThreadPoolThread '{1}'", callbackNumber, Thread.CurrentThread.IsThreadPoolThread);

                    Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

                    CustomLogger.LogMessage("Timer callback ended: '{0}'", callbackNumber);
                }
                finally
                {
                    Monitor.Exit(_timerCallbackLock);
                }
            }
            else
            {
                CustomLogger.LogMessage("Another timer callback is in progress");
            }                                    
        }

        public void Run()
        {
            CustomLogger.LogStartExample("OneByOne example: using lock");

            TimerCallbackState state = new TimerCallbackState();
            Timer timer = new Timer(TimerCallback, state, TimeSpan.Zero, _timerInternal);
            using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
            {
                try
                {
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }
                finally
                {
                    timer.Dispose(manualResetEvent);
                    CustomLogger.LogMessage("Timer disposed");
                }

                manualResetEvent.WaitOne();
                CustomLogger.LogMessage("All callbacks completed");
            }

            CustomLogger.LogEndExample();
            Console.ReadLine();
        }
    }
}
