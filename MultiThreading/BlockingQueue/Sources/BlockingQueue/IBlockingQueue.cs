using System;

namespace Lectures
{
    interface IBlockingQueue<T> : IDisposable
    {
        void Enqueue(T obj);
        T Dequeue();
    }
}
