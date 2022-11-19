using System;
using WorkerStore;

namespace Marshalling
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.marshalbyrefobject?view=net-5.0

    class Example
    {
        public static void Main()
        {
            // Create an ordinary instance in the current AppDomain
            Worker localWorker = new Worker();
            localWorker.PrintDomain();

            // Create a new application domain, create an instance
            // of Worker in the application domain, and execute code there.
            AppDomain ad = AppDomain.CreateDomain("New domain");
            ad.Load("WorkerStore");

            Worker remoteWorker = (Worker)ad.CreateInstanceAndUnwrap(
                typeof(Worker).Assembly.FullName, "WorkerStore.Worker");

            remoteWorker.PrintDomain();
        }
    }
}
