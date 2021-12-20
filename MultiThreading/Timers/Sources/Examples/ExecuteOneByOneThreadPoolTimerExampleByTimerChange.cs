using System;
using System.Threading;
using Common;

namespace Timers
{    
    internal sealed class ExecuteOneByOneThreadPoolTimerExampleByTimerChange
    {
        private readonly TimeSpan _timerInternal = TimeSpan.FromSeconds(2);
        private Timer _timer;
        private readonly TimerCallbackState _timerCallbackState = new TimerCallbackState();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();        

        public ExecuteOneByOneThreadPoolTimerExampleByTimerChange()
        {                        
        }      

        
        private void TimerCallback(object state)
        {            
            Int32 callbackNumber = _timerCallbackState.GetNextCallbackNumber();
            CustomLogger.LogMessage("Timer callback started: '{0}', IsThreadPoolThread '{1}'", callbackNumber, Thread.CurrentThread.IsThreadPoolThread);

            Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

            CustomLogger.LogMessage("Timer callback ended: '{0}'", callbackNumber);

            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _timer.Change(_timerInternal, Timeout.InfiniteTimeSpan);
            }
            else
            {
                CustomLogger.LogMessage("Timer is cancelled. Don't schedule another callback run.");   
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("OneByOne example: using timer change");            

            using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
            {
                try
                {
                    _timer = new Timer(TimerCallback, _timerCallbackState, _timerInternal, Timeout.InfiniteTimeSpan);
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }
                finally
                {
                    _cancellationTokenSource.Cancel();
                    
                    _timer.Dispose(manualResetEvent);
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
