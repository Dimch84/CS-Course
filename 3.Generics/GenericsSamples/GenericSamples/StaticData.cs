using System;

namespace GenericSamples
{
    public class MyGenericClass<T>
    {
        public static int count;

        public void IncrementMe()
        {
            Console.WriteLine($"Incremented value is : {++count}");
        }
    }
}
