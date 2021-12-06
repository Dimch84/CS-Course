using System.Collections.Generic;
using System.Threading;

namespace Lectures
{
    public sealed class BlockingQueueWithAutoResetEvents<T> : IBlockingQueue<T>
    {
        private readonly Queue<T> m_queue = new Queue<T>();
        private readonly Mutex m_mutex = new Mutex();
        private readonly AutoResetEvent m_event = new AutoResetEvent(false);

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
            }
            finally
            {
                m_mutex.ReleaseMutex();
            }
            m_event.Set();
        }

        public T Dequeue()
        {
            bool taken = true;
            m_mutex.WaitOne();
            try
            {
                while (m_queue.Count == 0)
                {
                    taken = false;
                    WaitHandle.SignalAndWait(m_mutex, m_event);
                    m_mutex.WaitOne();
                    taken = true;
                }
                return m_queue.Dequeue();
            }
            finally
            {
                if (taken)
                    m_mutex.ReleaseMutex();
            }
        }
    }
}
