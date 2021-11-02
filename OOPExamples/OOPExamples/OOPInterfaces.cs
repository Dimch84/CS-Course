using System;
using System.Text;
using NUnit.Framework;

namespace Interfaces
{
    public class OOPInterfaces
    {
		#region Example 1

		public interface IShape
		{
			void GetShape();
		}

		public class Circle : IShape
		{
			void IShape.GetShape()
			{
				GetShape();
			}

			protected void GetShape()
			{
				Console.WriteLine("Circle");
			}
		}

		public class MyShape : Circle, IShape
		{
			public void GetShape()
			{
				((IShape)this).GetShape();
			}
		}

		[Test]
		public void Test1()
		{
			MyShape figure = new MyShape();
			figure.GetShape();
		}

		#endregion

		#region Example 2

		interface I
		{
			void F1();
			void F2();
		}

		class A : I
		{
			public virtual void F1() { Console.WriteLine("A.F1()"); }
			public virtual void F2() { Console.WriteLine("A.F2()"); }
		}

		class B : A
		{
			public override void F1() { Console.WriteLine("B.F1()"); }
			public new void F2() { Console.WriteLine("B.F2()"); }
		}

		[Test]
		public void Test2()
		{
			A a1 = new B();
			a1.F1();
			a1.F2();

			A a2 = new A();
			I i = a2 as B;
			i.F1();
			i.F2();
		}

		#endregion

		#region Example 3

		public interface IProcessor
		{
			void DoWork();
		}

		public class DigitalProc : IProcessor
		{
			void IProcessor.DoWork()
			{
				DoWork();
			}

			protected void DoWork()
			{
				Console.WriteLine("Doing work.");
			}
		}

		public class MyDigitalProc : DigitalProc, IProcessor
		{
			public void DoWork()
			{
				((IProcessor)this).DoWork();
				Console.WriteLine("My proc doing work.");
			}
		}

		[Test]
		public static void Test3()
		{
			new MyDigitalProc().DoWork();
		}

		#endregion

		#region Example 4

		class Base : IDisposable
		{
			public virtual void Dispose()
			{
				Console.WriteLine("Base Dispose");
			}
		}

		class Derived : Base, IDisposable
		{
			public override void Dispose()
			{
				Console.WriteLine("Derived Dispose");
			}
		}

		[Test]
		public static void Test4()
		{
			Base b = new Base();
			((IDisposable)b).Dispose();

			Derived d = new Derived();
			((IDisposable)d).Dispose();

			b = new Derived();
			b.Dispose();
			((IDisposable)b).Dispose();
		}

		#endregion

		#region Example 5

		interface IWeight
		{
			void GetValue();
		}
		interface ILength
		{
			void GetValue();
		}

		public class MyObject : IWeight, ILength
		{
			void IWeight.GetValue()
			{
				Console.WriteLine("IWeight.GetValue");
			}

			void ILength.GetValue()
			{
				Console.WriteLine("ILength.GetValue");
			}

			public void GetValue()
			{
				Console.WriteLine("GetValue");
			}
		}

		[Test]
		public static void Test5()
		{
			MyObject myObj = new MyObject();

			myObj.GetValue();

			IWeight weight = myObj;
			weight.GetValue();

			ILength length = myObj;
			length.GetValue();
		}

		#endregion

		#region Example 6

		class MyType : IDisposable
		{
			public void Dispose() { Console.WriteLine("public Dispose"); }
			void IDisposable.Dispose() { Console.WriteLine("IDisposable Dispose"); }
		}

		[Test]
		public static void Test6()
		{
			MyType t = new MyType();
			t.Dispose();
			IDisposable d = t;
			d.Dispose();

			using (var td = new MyType()) { }
		}

		#endregion
	}
}
