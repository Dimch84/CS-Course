using System;
using System.Threading;
using Common;

namespace Events
{
    internal sealed class WaitAnyExample : IDisposable
    {
        private enum WaitAnyEventHandleTypes
        {
            EWorker = 0,
            EStopEvent  = 1,
            EEventCount = 2
        }

        private readonly AutoResetEvent _workerEvent;       
        private readonly ManualResetEvent _stopEvent;

        public WaitAnyExample()
        {
            _workerEvent = new AutoResetEvent(initialState: false);           
            _stopEvent = new ManualResetEvent(initialState: false);
        }

        public void Dispose()
        {
            _workerEvent.Dispose();
            _stopEvent.Dispose();
        }

        void ThreadRoutine()
        {
            try
            {
                CustomLogger.LogMessage("Thread: start");
                WorkingRoutine();
                CustomLogger.LogMessage("Thread: end");
            }
            catch(Exception exc)
            {
                CustomLogger.LogException(exc);
            }
        }

        void WorkingRoutine()
        {
            WaitHandle[] handles = new WaitHandle[] { _workerEvent, _stopEvent };

            while (true)
            {
                // when one of handles is set then WaitAny is unblocked
                // and that handle becomes reset (i.e. closed)
                WaitAnyEventHandleTypes result = (WaitAnyEventHandleTypes)WaitHandle.WaitAny(handles);
                CustomLogger.LogMessage("WaitAny return '{0}'", result);

                switch (result)
                {
                    case WaitAnyEventHandleTypes.EWorker:
                        CustomLogger.LogMessage("do work");
                        break;
                    case WaitAnyEventHandleTypes.EStopEvent:
                        CustomLogger.LogMessage("StopEvent signaled");
                        return;
                    default:
                        throw new Exception("Unknown event type");
                }
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("WaitAnyExample");

            try
            {
                Thread thread1 = new Thread(ThreadRoutine);
                thread1.Start();

                for (int i = 0; i < 3; ++i)
                {
                    Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
                    _workerEvent.Set();
                    CustomLogger.LogMessage("Main Thread: work event set");
                }               

                Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
                _stopEvent.Set();
                CustomLogger.LogMessage("Main Thread: stop event set");

                thread1.Join();
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage(exc.Message);
            }
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
