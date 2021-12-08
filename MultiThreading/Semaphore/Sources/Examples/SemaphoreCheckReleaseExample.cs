using System;
using System.Threading;
using Common;

namespace Lectures
{
    class SemaphoreCheckReleaseExample : IDisposable
    {
        private readonly Semaphore _semaphore;

        public SemaphoreCheckReleaseExample()
        {
            _semaphore = new Semaphore(0, 1);
        }

        public void Dispose()
        {
            _semaphore.Dispose();
        }

        public void Run()
        {
            ReleaseFromAnotherThread();

            ReleaseAlreadyFull();
        }

        private void ReleaseAlreadyFull()
        {
            CustomLogger.LogStartExample("SemaphoreCheckReleaseExample. Release already full semaphore");

            try
            {
                using (Semaphore semaphore = new Semaphore(1, 1))
                {
                    //semaphore.WaitOne();
                    //semaphore.Release();

                    semaphore.Release();
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }

        private void Routine()
        {
            try
            {
                CustomLogger.LogMessage("Before Release()");
                
                _semaphore.Release();

                CustomLogger.LogMessage("After Release()");
            }
            catch (Exception ex)
            {
                CustomLogger.LogMessage("Release semaphore from another thread", ex);
            }
        }

        public void ReleaseFromAnotherThread()
        {
            CustomLogger.LogStartExample("SemaphoreCheckReleaseExample. Release from another thread");

            var thread = new Thread(Routine);
            thread.Start();
            thread.Join();

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
