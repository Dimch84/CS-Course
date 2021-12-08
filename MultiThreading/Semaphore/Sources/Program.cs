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
                using (SemaphoreOrderExample semaphoreOrder = new SemaphoreOrderExample())
                {
                    semaphoreOrder.Run();
                }

                using (SemaphoreCheckReleaseExample example = new SemaphoreCheckReleaseExample()) 
                {
                    example.Run();
                }

                {
                    NamedSemaphoreExample example = new NamedSemaphoreExample();
                    example.Run();
                }

                {
                    NamedSemaphoreWithMutexExample example = new NamedSemaphoreWithMutexExample();
                    example.Run();
                }

                using (AbandonedSemaphoreExample example = new AbandonedSemaphoreExample())
                {
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
