using System;
using System.Text;
using NUnit.Framework;

namespace OOP
{
	[TestFixture]
	public class OOPTests
	{
		#region Example 1

		public class PlatformSUV
		{
			protected StringBuilder serialNo;

			public PlatformSUV()
			{
				Console.WriteLine("SUV platform created");

				CarChassisInitialize();

				serialNo = new StringBuilder();
				serialNo.Append("ABCD");
			}

			public virtual void CarChassisInitialize()
			{
				Console.WriteLine("SUV chassis installed");
			}

			public string SerialNo => serialNo.ToString();
		}

		public class LandCruiser : PlatformSUV
		{
			public LandCruiser()
			{
				Console.WriteLine("LandCruiser created");
			}

			public override void CarChassisInitialize()
			{
				serialNo.Append("-12345");
				Console.WriteLine("LandCruiser chassis installed");
			}
		}
		
		[Test]
		public void Test1()
		{
			LandCruiser newSUV = new LandCruiser();
			Console.WriteLine(newSUV.SerialNo);
		}

		#endregion

		#region Example 2

		class KeepSecret
		{
			private int someSecret;

			public KeepSecret(int someSecret)
			{
				this.someSecret = someSecret;
			}

			public bool Equals(KeepSecret other)
			{
				return other.someSecret == someSecret;
			}
		}

		// Private переменная экземпляра класса может быть доступна в другом экземпляре данного класса
		[Test]
		public void Test2()
		{
			KeepSecret ks = new KeepSecret(3);

			Console.WriteLine(ks.Equals(new KeepSecret(3)));
		}

		#endregion

		#region Example 3

		public class CBase
		{
			//private CBase()
			protected CBase()
			{
				Console.WriteLine("Base");
			}
		}

		public class CInherited : CBase
		{
			public CInherited()
			{
				Console.WriteLine("Inherited");
			}
		}

		[Test]
		public static void Test3()
		{
			CBase v = new CInherited();
		}

		#endregion

		#region Example 4

		class Car
		{
			private string _name;

			public Car(string name)
			{
				_name = name;
			}

			public string getName()
			{
				return _name;
			}

			public static void RebrandCar(ref Car car)
			{
				car = new Car("Volga");
			}
		}

		[Test]
		public static void Test4()
		{
			Car car = new Car("Lada");
			Car.RebrandCar(ref car);

			Console.WriteLine(car.getName());
		}

		#endregion

		#region Example 5

		class Vehicle
		{
			public int MaxSpeed;
			public static string Country;

			public Vehicle(int maxSpeed)
			{
				this.MaxSpeed = maxSpeed;
				Country = "Russian Federation";
			}
		}

		class Car5 : Vehicle
		{
			public Car5(int maxSpeed) : base(maxSpeed)
			{
				this.MaxSpeed = 110;
			}

			static Car5()
			{
				Country = "Czech Republic";
			}
		}

		[Test]
		public static void Test5()
		{
			var car = new Car5(130);
			Console.WriteLine(string.Format("Max Speed is {0}, Country is {1}", car.MaxSpeed, Vehicle.Country));
		}

		#endregion

		#region Example 7

		class Singleton
		{
			static readonly Singleton _instance = new Singleton();

			public static Singleton Instance { get { return _instance; } }

			private Singleton()
			{
				Console.WriteLine("Instance Constructor");
			}

			static Singleton()
			{
				Console.WriteLine("Static Constructor");
			}
		}

		[Test]
		public static void Test7()
		{
			var test = Singleton.Instance;
		}

		#endregion

		#region Example 8

		public abstract class A8
		{
			public virtual string Print() { return "A"; }
		}

		public abstract class B8 : A8
		{
			public virtual new string Print() { return "B"; }
		}

		public class C8 : B8
		{
			public override string Print() { return "C"; }
		}

		[Test]
		public static void Test8()
		{
			A8 ac = new C8();
			Console.WriteLine(ac.Print());
		}

		#endregion

		#region Example 8.2

		public abstract class A82
		{
			public virtual string Print() { return "A"; }
		}

		public abstract class B82 : A82
		{
			public override string Print() { return "B"; }
		}

		public class C82 : B82
		{
			public virtual new string Print() { return "C"; }
		}

		[Test]
		public static void Test8_2()
		{
			A82 ac = new C82();
			Console.WriteLine(ac.Print());
		}

		#endregion
	}
}
