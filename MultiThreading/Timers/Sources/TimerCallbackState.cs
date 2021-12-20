using System;
using System.Threading;

namespace Timers
{
    class TimerCallbackState
    {
        private Int32 _callbackCount;

        public Int32 GetNextCallbackNumber()
        {
            return Interlocked.Increment(ref _callbackCount);
        }
    }
}
