using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal sealed class BlockingQueueExampleCheckEmpty
    {
        private readonly IBlockingQueue<String> _queue;

        public BlockingQueueExampleCheckEmpty(IBlockingQueue<String> queue)
        {
            _queue = queue;
        }

        private void ConsumerThread()
        {
            CustomLogger.LogMessage("Consumer: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            CustomLogger.LogMessage("Consumer: '{0}' trying to read", Thread.CurrentThread.ManagedThreadId);
            String item1 = _queue.Dequeue();
            CustomLogger.LogMessage("Consumer: '{0}' read '{1}'", Thread.CurrentThread.ManagedThreadId, item1);

            CustomLogger.LogMessage("Consumer: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        private void ProducerThread()
        {
            CustomLogger.LogMessage("Producer: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(7));

            CustomLogger.LogMessage("Producer: '{0}' enqueue", Thread.CurrentThread.ManagedThreadId);
            _queue.Enqueue("1");

            CustomLogger.LogMessage("Producer: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Blocking queue check empty example");

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
