using System;
using System.Threading;

namespace Common
{
    public static class CustomTimeouts
    {
        public static TimeSpan ShortTimePeriod = TimeSpan.FromSeconds(1);
        public static TimeSpan MiddleTimePeriod = TimeSpan.FromSeconds(5);
        public static TimeSpan InfiniteTimePeriod = new TimeSpan(0, 0, 0, 0, Timeout.Infinite);
    }
}
