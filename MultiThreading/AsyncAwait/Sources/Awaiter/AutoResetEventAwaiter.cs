using Common;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AsyncAwait
{
    public static class AutoResetEventExtensions
    {
        public static AutoResetEventAwaiter GetAwaiter(this AutoResetEvent source)
        {
            return new AutoResetEventAwaiter(source);
        }
    }

    public sealed class AutoResetEventAwaiter : INotifyCompletion
    {
        private readonly AutoResetEvent _autoResetEvent;
        public AutoResetEventAwaiter(AutoResetEvent autoResetEvent)
        {
            _autoResetEvent = autoResetEvent;
        }
        public void OnCompleted(Action continuation)
        {
            CustomLogger.LogMessage("OnCompleted started");
            //ThreadPool.QueueUserWorkItem((obj) => continuation());
            continuation();
            CustomLogger.LogMessage("OnCompleted ended");
        }
        public bool IsCompleted { get; private set; }
        public void GetResult()
        {
            CustomLogger.LogMessage("GetResult started");
            _autoResetEvent.WaitOne();
            IsCompleted = true;
            CustomLogger.LogMessage("GetResult ended");
        }
    }

}
