using System;
using System.Collections;
using NUnit.Framework;
using GenericSamples;
using System.Collections.Generic;

namespace GenerictsSample
{
    [TestFixture]
    public class TestGenerics
    {
        [Test]
        public void NonGenericTest1()
        {
            Console.WriteLine("***A non-generic program demonstration.***");
            
            NonGenericEx nonGenericOb = new NonGenericEx();
            Console.WriteLine("DisplayMyInteger returns :{0}", nonGenericOb.DisplayMyInteger(123));
            Console.WriteLine("DisplayMyString returns :{0}", nonGenericOb.DisplayMyString("DisplayMyString method inside NonGenericEx is called."));
        }

        [Test]
        public void GenericTest1()
        {
            Console.WriteLine("***Introduction to Generic Programming.***");
            
            GenericClassDemo<int> myGenericClassIntOb = new GenericClassDemo<int>();
            Console.WriteLine("Display method returns :{0}", myGenericClassIntOb.Display(1));
            
            GenericClassDemo<string> myGenericClassStringOb = new GenericClassDemo<string>();
            Console.WriteLine("Display method returns :{0}", myGenericClassStringOb.Display("A generic method is called."));
            
            GenericClassDemo<double> myGenericClassDoubleOb = new GenericClassDemo<double>();
            Console.WriteLine("Display method returns :{0}", myGenericClassDoubleOb.Display(12.345));
        }

        [Test]
        public void NonGenericTest2()
        {
            Console.WriteLine("***Use Generics to avoid runtime error***");
            
            ArrayList myList = new ArrayList();
            myList.Add(1);
            myList.Add(2);
            //No compile time error
            myList.Add("InvalidElement");
            foreach (int myInt in myList)
            {
                //Will encounter run-time exception for the final element which is not an int
                Console.WriteLine((int)myInt); //downcasting
            }
        }

        [Test]
        public void GenericTest2()
        {
            Console.WriteLine("***Using Generics to avoid run-time error.***");
            List<int> myList = new List<int>();
            myList.Add(1);
            myList.Add(2);
            //Compile time error: Cannot convert from 'string' to 'int'
            //myList.Add("InvalidElement");
            foreach (int myInt in myList)
            {
                Console.WriteLine((int)myInt);//downcasting
                //Additional information: Cannot modify the collection
                //myList.Add(15); //Run-time exception
            }
        }

        [Test]
        public void GenericInterfaceTest()
        {
            Console.WriteLine("***Implementing generic interfaces.***\n");
            //Using 'int' type
            GenericInterface<int> concreteInt = new ConcreteClass<int>();
            int myInt = concreteInt.GenericMethod(5);
            Console.WriteLine($"The value stored in myInt is : {myInt}");
            concreteInt.NonGenericMethod();

            //Using 'string' type now
            GenericInterface<string> concreteString = new ConcreteClass<string>();
            string myStr = concreteString.GenericMethod("Hello Reader");
            Console.WriteLine($"The value stored in myStr is : {myInt}");
            concreteString.NonGenericMethod();
        }

        [Test]
        public void MethodOverrideTest()
        {
            Console.WriteLine("***Overriding a virtual method.***\n");
            
            BaseClass<int> intBase = new BaseClass<int>();
            //Invoking Parent class method
            Console.WriteLine($"Parent class method returns {intBase.MyMethod(25)}");//25
            //Now pointing to the child class method and invoking it.
            intBase = new DerivedClass<int>();
            Console.WriteLine($"Derived class method returns {intBase.MyMethod(25)}");//25
            
            //The following will cause compile-time error
            //intBase = new DerivedClass<double>();//error
        }

        [Test]
        public void StaticDataTest()
        {
            Console.WriteLine("***Testing static in the context of generic programming.***");
            MyGenericClass<int> intOb = new MyGenericClass<int>();
            Console.WriteLine("\nUsing intOb now.");
            intOb.IncrementMe();//1
            intOb.IncrementMe();//2
            intOb.IncrementMe();//3

            Console.WriteLine("\nUsing strOb now.");
            MyGenericClass<string> strOb = new MyGenericClass<string>();
            strOb.IncrementMe();//1
            strOb.IncrementMe();//2

            Console.WriteLine("\nUsing doubleOb now.");
            MyGenericClass<double> doubleOb = new MyGenericClass<double>();
            doubleOb.IncrementMe();//1
            doubleOb.IncrementMe();//2

            MyGenericClass<int> intOb2 = new MyGenericClass<int>();
            Console.WriteLine("\nUsing intOb2 now.");
            intOb2.IncrementMe();//4
            intOb2.IncrementMe();//5           
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
