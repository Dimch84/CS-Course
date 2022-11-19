using System;

namespace WorkerStore
{
    public class Worker : MarshalByRefObject
    {
        public void PrintDomain()
        {
            Console.WriteLine("Object is executing in AppDomain \"{0}\"",
                AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
