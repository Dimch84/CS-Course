using System;
using System.Text;
using NUnit.Framework;

namespace OOPBase
{
    public class OOPBase
    {
        #region Example 1

        public class A
        {
            public A()
            {
                Console.WriteLine("This is A");
            }
        }

        public class B : A
        {
            public B()
            {
                Console.WriteLine("This is B");
            }
        }

        [Test]
        public void Test1()
        {
            B b = new B();
        }

        #endregion

        #region Example 2

        public class Base
        {
            public virtual void Foo(int x)
            {
                Console.WriteLine("Base.Foo(int)");
            }
        }
        public class Derived : Base
        {
            public override void Foo(int x)
            {
                Console.WriteLine("Derived.Foo(int)");
            }
            public void Foo(object o)
            {
                Console.WriteLine("Derived.Foo(object)");
            }
        }

        [Test]
        public void Test2()
        {
            Base d = new Derived();
            int i = 10;
            d.Foo(i);
        }

        #endregion

        #region Example 3

        class A3
        {
            static A3()
            {
                Console.WriteLine("Static Hello from A3");
            }

            public A3()
            {
                Console.WriteLine("Hello from A3");
            }
        }

        class B3
        {
            public static string x = "Hello";
            static B3()
            {
                Console.WriteLine("Static Hello from B3");
            }

            public B3()
            {
                Console.WriteLine("Hello from B3");
            }
        }

        [Test]
        public void Test3()
        {
            A3 a = new A3();
            A3 a2 = new A3();
            Console.WriteLine(B3.x);
        }

        #endregion

        #region Example 4

        public class A4
        {
            public virtual string Do()
            {
                return "Class A4";
            }
        }

        public class B4 : A4
        {
            public override string Do()
            {
                return "Class B4";
            }
        }

        public class C4 : B4
        {
            public new string Do()
            {
                return "Class C4";
            }
        }

        [Test]
        public void Test4()
        {
            A4 a = new C4();
            Console.WriteLine(a.Do());
        }

        #endregion
    }
}
