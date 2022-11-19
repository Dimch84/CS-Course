using System.Collections.Generic;
using System.Threading;

namespace Lectures
{
    public sealed class BlockingQueueWithManualResetEvents<T> : IBlockingQueue<T>
    {
        private readonly Queue<T> m_queue = new Queue<T>();
        private readonly Mutex m_mutex = new Mutex();
        private readonly ManualResetEvent m_event = new ManualResetEvent(false);

        public void Dispose()
        {
            m_mutex.Dispose();
            m_event.Dispose();
        }

        public void Enqueue(T obj)
        {
            m_mutex.WaitOne();
            try
            {
                m_queue.Enqueue(obj);
                if (m_queue.Count == 1)
                    m_event.Set();
            }
            finally
            {
                m_mutex.ReleaseMutex();
            }
        }

        public T Dequeue()
        {
            T value;
            bool taken = true;
            m_mutex.WaitOne();
            try
            {
                while (m_queue.Count == 0)
                {
                    taken = false;
                    m_mutex.ReleaseMutex();
                    m_event.WaitOne();
                    m_mutex.WaitOne();
                    taken = true;
                }
                value = m_queue.Dequeue();

                if (m_queue.Count == 0)
                    m_event.Reset();
            }
            finally
            {
                if (taken)
                    m_mutex.ReleaseMutex();
            }
            return value;
        }
    }
}
