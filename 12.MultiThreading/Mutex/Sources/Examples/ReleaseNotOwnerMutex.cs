using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class ReleaseNotOwnerMutex : IDisposable
    {
        private readonly Mutex _mutex;

        public ReleaseNotOwnerMutex()
        {
            _mutex = new Mutex(false);
        }

        public void Dispose()
        {
            _mutex.Dispose();
        }
      

        public void Run()
        {
            CustomLogger.LogStartExample("ReleaseNotOwnedMutexExample");

            try
            {
                _mutex.ReleaseMutex();
            }
            catch(Exception exc)
            {
                CustomLogger.LogException(exc);
            }
                      
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
