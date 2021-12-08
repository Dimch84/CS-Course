using System;

namespace Generations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GenerationTest();

            Console.ReadKey();
            Console.WriteLine();

            GenerationTest2();

            Console.ReadKey();
            Console.WriteLine();

            GCTotalMemoryTest();

            Console.ReadKey();
        }

        private static void GenerationTest()
        {
            String myString = "Hello, world!";
            Console.WriteLine("New object is created");

            Int32 currentGeneration = GC.GetGeneration(myString);
            Console.WriteLine("Generation of an object is: {0}", currentGeneration);

            Console.WriteLine("Performing GC");
            GC.Collect();
            GC.WaitForFullGCComplete();

            currentGeneration = GC.GetGeneration(myString);
            Console.WriteLine("Generation of an object is: {0}", currentGeneration);

            Console.WriteLine("Performing GC");
            GC.Collect();
            GC.WaitForFullGCComplete();

            currentGeneration = GC.GetGeneration(myString);
            Console.WriteLine("Generation of an object is: {0}", currentGeneration);

            Console.WriteLine("Performing GC");
            GC.Collect();
            GC.WaitForFullGCComplete();

            currentGeneration = GC.GetGeneration(myString);
            Console.WriteLine("Generation of an object is: {0}", currentGeneration);
        }

        private static void GenerationTest2()
        {
            LogArray(new Byte[84000]);
            LogArray(new Byte[86000]);

            Console.WriteLine();

            LogArray(new Double[999]);
            LogArray(new Double[1000]);
        }

        private static void LogArray<T>(T[] array)
        {
            Int32 arrayGeneration = GC.GetGeneration(array);
            Console.WriteLine("Array of {0} length is {1} and current generation is {2}", typeof(T), array.Length, arrayGeneration);
        }

        private static void GCTotalMemoryTest()
        {
            GC.Collect();
            Console.WriteLine("Total bytes before allocation: {0}", GC.GetTotalMemory(false));
            Customer cust = new Customer();
            Console.WriteLine("Total bytes after allocation: {0}", GC.GetTotalMemory(false));
            Console.WriteLine("Object is at generation {0}", GC.GetGeneration(cust));

            Console.WriteLine("Collect");
            GC.Collect();
            Console.WriteLine("Total bytes after collection: {0}", GC.GetTotalMemory(false));
            Console.WriteLine("Object is at generation {0}", GC.GetGeneration(cust));

            Console.WriteLine("Deallocating object");
            cust = null;
            Console.WriteLine("Collect");
            GC.Collect();
            Console.WriteLine("Total bytes after collection: {0}", GC.GetTotalMemory(false));

            Console.WriteLine("Collect");
            GC.Collect();
            Console.WriteLine("Total bytes after collection: {0}", GC.GetTotalMemory(false));
        }
    }
}
