using Common;
using System;
using System.Threading;

namespace Timers
{
    internal sealed class BasicThreadPoolTimerExample
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
            CustomLogger.LogStartExample("ThreadPool Timer example");

            TimerCallbackState state = new TimerCallbackState();
            using (Timer timer = new Timer(TimerCallback, state, TimeSpan.Zero, _timerInternal))
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }

            CustomLogger.LogEndExample();
            Console.ReadLine();
        }
    }
}
