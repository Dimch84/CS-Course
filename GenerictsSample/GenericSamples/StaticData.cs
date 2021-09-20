using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
