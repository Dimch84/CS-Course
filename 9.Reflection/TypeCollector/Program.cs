using System;

namespace TypeCollector
{
    interface IShape
    {
        double GetSquare();
    }

    abstract class InOut
    {
        public abstract void Read();
        public abstract void Write(string s);
    }

    class Circle : IShape 
    {
        public double GetSquare()
        {
            return 0;
        }
    }

    class Rectangle : IShape
    {
        public double GetSquare()
        {
            return 1;
        }
    }

    class Triangle : IShape
    {
        public double GetSquare()
        {
            return 2;
        }
    }

    class ConsoleIO : InOut 
    {
        public override void Read()
        {
            Console.Read();
        }
        public override void Write(string s)
        {
            Console.Write(s);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            TypeCollector tc = new TypeCollector();

            var allObjectsInheritingTheInterface = tc.GetAllInheritingTypes<IShape>();
            foreach(var t in allObjectsInheritingTheInterface)
            {
                Console.WriteLine(t);
            }

            Console.WriteLine("\nInherited from abstract Classes:");

            var allObjectsInheritingTheAbstractClass = tc.GetAllInheritingTypes<InOut>();
            foreach (var t in allObjectsInheritingTheAbstractClass)
            {
                Console.WriteLine(t);
            }

            Console.ReadLine();
        }
    }
}
