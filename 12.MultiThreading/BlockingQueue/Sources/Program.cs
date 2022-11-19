using System;
using Common;
using Lectures;

namespace BlockingQueue
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CustomLogger.LogMessage("Blocking queue with semaphores");
                using (BlockingQueueSemaphore<String> queue = new BlockingQueueSemaphore<string>(2))
                {
                    new BlockingQueueExampleCheckEmpty(queue).Run();

                    new BlockingQueueExampleCheckLimit(queue).Run();
                }

                CustomLogger.LogMessage("Blocking queue with semaphores slim");
                using (BlockingBoundedSemaphoreSlim<String> queue = new BlockingBoundedSemaphoreSlim<string>(2))
                {
                    new BlockingQueueExampleCheckEmpty(queue).Run();

                    new BlockingQueueExampleCheckLimit(queue).Run();
                }

                CustomLogger.LogMessage("Blocking queue with auto reset event");
                using (BlockingQueueWithAutoResetEvents<String> queue = new BlockingQueueWithAutoResetEvents<string>())
                {
                    new BlockingQueueExampleCheckEmpty(queue).Run();

                    new BlockingQueueExampleCheckLimit(queue).Run();
                }

                CustomLogger.LogMessage("Blocking queue with manual reset event");
                using (BlockingQueueWithManualResetEvents<String> queue = new BlockingQueueWithManualResetEvents<string>())
                {
                    new BlockingQueueExampleCheckEmpty(queue).Run();

                    new BlockingQueueExampleCheckLimit(queue).Run();
                }

                CustomLogger.LogMessage("Blocking queue with monitor");
                using (BlockingQueueWithMonitor<String> queue = new BlockingQueueWithMonitor<string>())
                {
                    new BlockingQueueExampleCheckEmpty(queue).Run();

                    new BlockingQueueExampleCheckLimit(queue).Run();
                }

                CustomLogger.LogMessage("Blocking queue with monitor advanced");
                using (BlockingQueueWithMonitor2<String> queue = new BlockingQueueWithMonitor2<string>(2))
                {
                    new BlockingQueueExampleCheckEmpty(queue).Run();

                    new BlockingQueueExampleCheckLimit(queue).Run();
                }

                CustomLogger.LogMessage("Blocking queue with blocking collection");
                using (BlockingQueueWithCollection<String> queue = new BlockingQueueWithCollection<string>(2))
                {
                    new BlockingQueueExampleCheckEmpty(queue).Run();

                    new BlockingQueueExampleCheckLimit(queue).Run();
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogException(e);
            }
        }
    }
}
