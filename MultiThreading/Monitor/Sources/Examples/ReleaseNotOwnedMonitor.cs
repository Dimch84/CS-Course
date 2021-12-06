using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class ReleaseNotOwnedExample : IDisposable
    {
        private readonly Object _object = new Object();

        public ReleaseNotOwnedExample()
        {
        }

        public void Dispose()
        {
        }

        public void Run()
        {
            CustomLogger.LogStartExample("ReleaseNotOwnedExample");

            try
            {
                Monitor.Exit(_object);
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
