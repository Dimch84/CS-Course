using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal sealed class BlockingQueueExampleCheckLimit
    {
        private readonly IBlockingQueue<String> _queue;

        public BlockingQueueExampleCheckLimit(IBlockingQueue<String> queue)
        {
            _queue = queue;
        }

        private void ConsumerThread()
        {
            CustomLogger.LogMessage("Consumer: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(7));

            String item1 = _queue.Dequeue();
            CustomLogger.LogMessage("Consumer: '{0}' read '{1}'", Thread.CurrentThread.ManagedThreadId, item1);

            String item2 = _queue.Dequeue();
            CustomLogger.LogMessage("Consumer: '{0}' read '{1}'", Thread.CurrentThread.ManagedThreadId, item2);

            String item3 = _queue.Dequeue();
            CustomLogger.LogMessage("Consumer: '{0}' read '{1}'", Thread.CurrentThread.ManagedThreadId, item3);

            CustomLogger.LogMessage("Consumer: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        private void ProducerThread()
        {
            CustomLogger.LogMessage("Producer: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            _queue.Enqueue("1");

            CustomLogger.LogMessage("Producer: '{0}', enqueue 1", Thread.CurrentThread.ManagedThreadId);

            _queue.Enqueue("2");

            CustomLogger.LogMessage("Producer: '{0}', enqueue 2", Thread.CurrentThread.ManagedThreadId);

            _queue.Enqueue("3");

            CustomLogger.LogMessage("Producer: '{0}', enqueue 3", Thread.CurrentThread.ManagedThreadId);

            CustomLogger.LogMessage("Producer: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Blocking queue check limit example");

            Thread consumerThread = new Thread(ConsumerThread);
            consumerThread.Start();

            Thread producerThread = new Thread(ProducerThread);
            producerThread.Start();

            consumerThread.Join();
            producerThread.Join();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
