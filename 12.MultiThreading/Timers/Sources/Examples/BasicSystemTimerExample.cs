using System;
using System.Threading;
using System.Timers;
using Common;

namespace Timers
{    
    internal sealed class BasicSystemTimerExample
    {
        private TimerCallbackState _timerCallbackState = new TimerCallbackState();
        private void TimerCallback(object sender, ElapsedEventArgs e)
        {             
            Int32 callbackNumber = _timerCallbackState.GetNextCallbackNumber();
            CustomLogger.LogMessage("Timer callback started: '{0}', IsThreadPoolThread '{1}'", 
                callbackNumber, Thread.CurrentThread.IsThreadPoolThread);

            Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

            CustomLogger.LogMessage("Timer callback ended: '{0}'", callbackNumber);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("ThreadPool Timer example");

            using (System.Timers.Timer tmr = new System.Timers.Timer())
            {
                tmr.Interval = 2000;
                tmr.Elapsed += TimerCallback;
                tmr.Start();

                Thread.Sleep(TimeSpan.FromSeconds(10));
            }            

            CustomLogger.LogEndExample();
            Console.ReadLine();
        }

    }
}
