using Common;
using System;
using System.Threading;

namespace Timers
{
    internal sealed class WaitAllBeforeExitThreadPoolTimerExample
    {
        private readonly TimeSpan _timerInternal = TimeSpan.FromSeconds(2);
        private void TimerCallback(object state)
        {
            TimerCallbackState timerCallbackState = (TimerCallbackState)state;
            Int32 callbackNumber = timerCallbackState.GetNextCallbackNumber();
            CustomLogger.LogMessage("Timer callback started: '{0}', IsThreadPoolThread '{1}'", callbackNumber, Thread.CurrentThread.IsThreadPoolThread);

            Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

            CustomLogger.LogMessage("Timer callback ended: '{0}'", callbackNumber);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Wait all callbacks example");

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
