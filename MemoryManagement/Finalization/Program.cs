using System;

namespace Finalization
{
    internal class Program
    {
        private static void Main()
        {
            FinalizableTest();

            Console.WriteLine();

            DisposeTest();
        }

        private static void DisposeTest()
        {
            using (new MyDisposableClass())
            {
                Console.WriteLine("Working with unmanaged resource");
            }

            GC.Collect();

            // To see that no finalization is performed
            GC.WaitForFullGCComplete();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Console.ReadKey();
        }

        private static void FinalizableTest()
        {
            MyDisposableClass finalizableClass = new MyDisposableClass();

            Console.WriteLine("Working with unmanaged resource");

            finalizableClass = null;

            GC.Collect();
            GC.WaitForFullGCComplete();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Console.ReadKey();
        }
    }

    internal class UnmanagedResource
    {
        public UnmanagedResource()
        {
            // take some unmanaged resource
            Console.WriteLine("Allocating unmanaged resource");
        }

        public void Clean()
        {
            // free some unmanaged resource
            Int32 generation = GC.GetGeneration(this);
            Console.WriteLine("Cleaning unmanaged resource in {0} generation", generation);
        }
    }
    
    internal class MyDisposableClass : IDisposable
    {
        private readonly UnmanagedResource _resource;
        private bool disposedValue = false; // To detect redundant calls

        public MyDisposableClass()
        {
            _resource = new UnmanagedResource();
        }

        public void Dispose()
        {
            Cleanup(true);
            GC.SuppressFinalize(this);
        }

        ~MyDisposableClass()
        {
            Cleanup(false);
        }

        private void Cleanup(Boolean disposing)
        {
            if (disposedValue)
                return;

            if (disposing)
            {
                // dispose managed state (managed objects).
            }

            // free unmanaged resources (unmanaged objects).
            _resource.Clean();

            disposedValue = true;
        }
    }
}
