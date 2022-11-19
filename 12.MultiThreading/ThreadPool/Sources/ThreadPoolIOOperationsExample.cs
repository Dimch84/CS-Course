using System;
using System.Threading;
using System.IO;
using Common;

namespace Lectures
{
    internal class ThreadPoolIOOperationsExample
    {
        private volatile bool _isRead;

        public void Thread1()
        {
            try
            {
                CustomLogger.LogMessage("Thread started '{0}'", Thread.CurrentThread.ManagedThreadId);

                byte[] buffer = new byte[1024];
                Int32 offset = 0;

                using (FileStream file = new FileStream("File1", FileMode.Open, FileAccess.Read, FileShare.Read, 1, true))
                {
                    int readBytes;
                    do
                    {
                        IAsyncResult asyncResult = file.BeginRead(buffer, offset, buffer.Length, null, null);

                        //Thread.Sleep(TimeSpan.FromSeconds(1));

                        readBytes = file.EndRead(asyncResult);
                    }
                    while (readBytes != 0);
                }

                _isRead = true;
                CustomLogger.LogMessage("Thread ended '{0}'", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage(exc.Message);
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("thread pool many threads example");

            Int32 workerThreads;
            Int32 completionPortThreads;
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            CustomLogger.LogMessage("Max Worker thread '{0}', max completion port thread '{1}'", workerThreads, completionPortThreads);

            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            CustomLogger.LogMessage("Min Worker thread '{0}', min completion port thread '{1}'", workerThreads, completionPortThreads);

            Thread thread = new Thread(Thread1);
            thread.Start();

            while (!_isRead)
            {
                ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
                CustomLogger.LogMessage("Available Worker thread '{0}', completion port thread '{1}'", workerThreads, completionPortThreads);

                Thread.Sleep(TimeSpan.FromMilliseconds(1));
            }
            thread.Join();

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
