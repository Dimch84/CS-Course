using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegateSamples
{
    delegate int DelegateWithTwoIntParameterReturnInt(int x, int y);

    [TestFixture]
    public class DelegateSample
    {
        public static int Sum(int a, int b)
        {
            return a + b;
        }

        [Test]
        public void TestDelegate()
        {
            DelegateWithTwoIntParameterReturnInt delOb = Sum;
            int total = delOb(10, 20);
            Assert.AreEqual(30, total);

            //Alternative way to calculate the aggregate of the numbers.
            total = delOb.Invoke(25, 75);
            Assert.AreEqual(100, total);
        }

        //
        class OutSideProgram
        {
            public int CalculateSum(int x, int y)
            {
                return x + y;
            }

            public void SimpleMethod()
            {
                Console.WriteLine("An instance method of OutsideProgram class is executed.");
            }
        }

        [Test]
        public void TestOutsideDelegate()
        {
            OutSideProgram outsideOb = new OutSideProgram();

            DelegateWithTwoIntParameterReturnInt delOb = outsideOb.CalculateSum;
            int total = delOb(50, 70);
            Assert.AreEqual(120, total);

            Console.WriteLine("delOb.Target={0}", delOb.Target);
            Console.WriteLine("delOb.Method={0}", delOb.Method);
        }

        //
        delegate void MultiDelegate();

        public static void MethodOne()
        {
            Console.WriteLine("A static method 1 executed");
        }

        public static void MethodTwo()
        {
            Console.WriteLine("A static method 1 executed");
        }

        [Test]
        public void TestMulticastDelegate()
        {
            MultiDelegate multiDel = MethodOne;
            // Target another static method
            multiDel += MethodTwo;
            //Target an instance method
            multiDel += () => new OutSideProgram().SimpleMethod();
            multiDel();

            //Reducing the delegate chain
            multiDel -= MethodTwo;

            //The following invocation will call MethodOne and MethodThree now.
            multiDel();
        }

        //
        delegate int IntMultiDelegate();

        public static int MethodOneInt()
        {
            Console.WriteLine("A static method MethodOne() executed.");
            return 1;
        }
        public static int MethodTwoInt()
        {
            Console.WriteLine("A static method MethodTwo() executed.");
            return 2;
        }
        public static int MethodThreeInt()
        {
            Console.WriteLine("A static method MethodThree() executed.");
            return 3;
        }

        [Test]
        public void TestMulticastDelegate2()
        {
            IntMultiDelegate multiDel = MethodOneInt;
            // Target MethodTwo
            multiDel += MethodTwoInt;
            // Target MethodThree
            multiDel += MethodThreeInt;

            int finalValue = multiDel();
            Console.WriteLine("The final value is {0}", finalValue);
        }

        #region CovarianceContravariance

        class Vehicle
        {
            public void ShowVehicle(Vehicle myVehicle)
            {
                Console.WriteLine("Vehicle.ShowVehicle is called.");
                Console.WriteLine("myVehicle.GetHashCode() is: {0}", myVehicle.GetHashCode());
            }
            public Vehicle CreateVehicle()
            {
                Vehicle myVehicle = new Vehicle();
                Console.WriteLine(" Inside Vehicle.CreateVehicle, a vehicle object is created.");
                return myVehicle;
            }
        }
        class Bus : Vehicle
        {
            public void ShowBus(Bus myBus)
            {
                Console.WriteLine("Bus.ShowBus is called.");
                Console.WriteLine("myBus.GetHashCode() is: {0}", myBus.GetHashCode());
            }
            public Bus CreateBus()
            {
                Bus myBus = new Bus();
                Console.WriteLine(" Inside Bus.CreateBus, a bus object is created.");
                return myBus;
            }
        }

        delegate void BusDelegate(Bus bus);

        [Test]
        public void TestContravariance()
        {
            Vehicle myVehicle = new Vehicle();
            Bus myBus = new Bus();
            //Normal case
            BusDelegate busDelegate = myBus.ShowBus;
            busDelegate(myBus);

            //Special case
            //Contravariance:
            /*
             * Note that the following delegate expected a method that accepts 
             a Bus(derived) object parameter but still it can point to the method 
             that accepts Vehicle(base) object parameter
             */
            BusDelegate anotherBusDelegate = myVehicle.ShowVehicle;
            anotherBusDelegate(myBus);
        }

        // Covariance test
        delegate Vehicle VehicleDelegate();

        [Test]
        public void TestCovariance()
        {
            Vehicle vehicleOb = new Vehicle();
            Bus busOb = new Bus();

            //Normal case:
            /* VehicleDelegate is expecting a method 
             with return type Vehicle.*/
            VehicleDelegate vehicleDelegate1 = vehicleOb.CreateVehicle;
            vehicleDelegate1();

            /*VehicleDelegate is expecting a method with return type Vehicle(i.e. a basetype)
            but you're assigning a method with return type Bus (a derived type)
            Covariance allows this kind of assignment.*/
            VehicleDelegate vehicleDelegate2 = busOb.CreateBus;
            vehicleDelegate2();
        }

        //
        public delegate object StrDelegate(string arg);

        // object
        //  /|\
        //   |
        // string
        static string /*covariance 'out'*/ MyMethod(object /*contravariance 'in'*/ s)
        {
            Console.WriteLine(s);

            return "Hi! " + s;
        }

        [Test]
        public void TestCoAndContraVariance()
        {
            StrDelegate testDlgt = MyMethod;
            var res = testDlgt("Test message");
            Assert.IsNotNull(res);

            Console.WriteLine(res);
        }

        #endregion
    }
}
