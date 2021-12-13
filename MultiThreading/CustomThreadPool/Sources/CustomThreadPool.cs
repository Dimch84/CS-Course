using System;

namespace _10.CustomThreadPool
{
    public class CustomThreadPool
    {
        private static readonly int CONCURRENCY_LEVEL = 10;
        private static readonly SuperQueue queue = new SuperQueue(CONCURRENCY_LEVEL);

        public static void QueueUserWorkItem(Action<object> action, object arg)
        {
            queue.EnqueueTask(new Task(action, arg));
        }

        public static void CleanUp()
        {
            queue.Dispose();
        }
    }
}
