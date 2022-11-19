using System;
using Common;

namespace Lectures
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (MonitorOrderExample example = new MonitorOrderExample())
                {
                    example.Run();
                }

                using (ReleaseNotOwnedExample example = new ReleaseNotOwnedExample())
                {
                    example.Run();
                }

                using (PulseWaitMonitorExample example = new PulseWaitMonitorExample())
                {
                    example.Run();
                }

                using (PulseWaitNotOwnedMonitorExample example = new PulseWaitNotOwnedMonitorExample())
                {
                    example.Run();
                }

                using (PulseBeforeWaitExample example = new PulseBeforeWaitExample())
                {
                    example.Run();
                }

                using (AbandonedMonitorExample example = new AbandonedMonitorExample())
                {
                    example.Run();
                }

                {
                    BadPracticeExample example = new BadPracticeExample();
                    example.Run();
                }

                using (FakeWakeUpExample example = new FakeWakeUpExample())
                {
                    example.Run();
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
            }
        }
    }
}
