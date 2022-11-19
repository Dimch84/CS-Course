using System;
using Common;
using Events.Sources;

namespace Events
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                using (AutoResetEventOrderExample example = new AutoResetEventOrderExample())
                {
                    example.Run();
                }

                using (AutoResetEventExample example = new AutoResetEventExample())
                {
                    example.Run();
                }

                using (AutoResetEventUseResetExample example = new AutoResetEventUseResetExample())
                {
                    example.Run();
                }


                using (ManualResetEventExample example = new ManualResetEventExample())
                {
                    example.Run();
                }

                using (CountDownEventExample example = new CountDownEventExample())
                {
                    example.Run();
                }

                using (WaitAnyExample example = new WaitAnyExample())
                {
                    example.Run();
                }

                {
                    NamedEventExample example = new NamedEventExample();
                    example.Run();
                }

                {
                    NamedDiffEventsExample example = new NamedDiffEventsExample();
                    example.Run();
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage(exc.Message);
            }
        }
    }
}
