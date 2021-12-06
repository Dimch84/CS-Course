using System;
using System.Threading;
using Common;

namespace Lectures
{
    class SemaphoreCheckReleaseExample
    {
        public void Run()
        {
            CustomLogger.LogStartExample("SemaphoreCheckReleaseExample");

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
    }
}
