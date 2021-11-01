using System;
using System.Collections.Generic;

namespace ClrMemoryManagement
{
    internal class Program
    {
        private static void Main()
        {
            Int32 i = 42;

            Object boxedValue = i;

            String boxedTypeName = boxedValue.GetType().ToString();
            Console.WriteLine("Boxed object type: {0}", boxedTypeName);
            Console.WriteLine();

            //Boolean isEquals = boxedValue.Equals(i);
            Boolean isEquals = i.Equals(boxedValue);
            Console.WriteLine("Boxed and original values are equal: {0}", isEquals);
            Console.WriteLine();

            Boolean refEqualsValue = Object.ReferenceEquals(i, i);
            Boolean refEqualsBoxedValue = Object.ReferenceEquals(boxedValue, boxedValue);
            Console.WriteLine("Self reference equalty of value type: {0}", refEqualsValue);
            Console.WriteLine("Self reference equalty of boxed value type: {0}", refEqualsBoxedValue);
            Console.WriteLine();

            Int32 unboxedValue = (Int32)boxedValue;
            Console.WriteLine("Unboxed value: {0}", unboxedValue);
            Console.WriteLine();

            //PrintReferenceType(i);
            PrintReferenceType(boxedValue);
            Console.WriteLine();

            ValueType valueTypeValue = i;
            PrintReferenceType(valueTypeValue);

            Console.ReadKey();
        }

        private static void PrintReferenceType<TRef>(TRef toPrint) where TRef : class
        {
            Console.WriteLine("String value of passed object: {0}, Type of passed object: {1}", toPrint, toPrint.GetType());
        }
    }
}
