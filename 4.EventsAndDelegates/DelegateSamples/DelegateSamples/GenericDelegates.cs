using NUnit.Framework;
using System;

namespace DelegateSamples
{
    //A generic contravariant delegate
    delegate void ContraDelegate<in T>(T t);
    //A generic non-contravariant delegate
    //delegate void ContraDelegate<T>(T t);

    class Vehicle
    {
        public virtual void ShowMe()
        {
            Console.WriteLine(" Vehicle.ShowMe()");
        }
    }
    class Bus : Vehicle
    {
        public override void ShowMe()
        {
            Console.WriteLine(" Bus.ShowMe()");
        }
    }

    [TestFixture]
    public class GenericDelegates
    {
        #region Contravariance delegate

        [Test]
        public void TestContravarianceGenericDelegate()
        {
            Vehicle obVehicle = new Vehicle();
            Bus obBus = new Bus();

            ContraDelegate<Vehicle> contraVehicle = ShowVehicleType;
            contraVehicle(obVehicle);
            
            ContraDelegate<Bus> contraBus = ShowBusType;
            contraBus(obBus);

            //Using general type to derived type.
            //Following assignment is Ok, if you use 'in' in delegate definition.
            //Otherwise, you'll receive compile-time error.
            contraBus = contraVehicle; // Ok
            contraBus(obBus);
        }

        private static void ShowVehicleType(Vehicle vehicle)
        {
            vehicle.ShowMe();
        }
        private static void ShowBusType(Bus bus)
        {
            bus.ShowMe();
        }

        #endregion

        #region Covariance delegate
        //A generic delegate with covariant return type
        //(Notice the use of 'out' keyword)
        delegate TResult CovDelegate<out TResult>();

        [Test]
        public void TestCovarianceGenericDelegate()
        {
            CovDelegate<Vehicle> covVehicle = GetOneVehicle;
            covVehicle();

            CovDelegate<Bus> covBus = GetOneBus;
            covBus();
            
            //Testing Covariance
            //covBus to covVehicle (i.e. more specific-> more general) is allowed through covariance

            //Following assignment is Ok, if you use 'out' in delegate definition
            //Otherwise, you'll receive compile-time error
            covVehicle = covBus; // Still Ok
            covVehicle();
        }

        private static Vehicle GetOneVehicle()
        {
            Console.WriteLine("Creating one vehicle and returning it.");
            return new Vehicle();
        }
        private static Bus GetOneBus()
        {
            Console.WriteLine("Creating one bus and returning the bus.");
            return new Bus();
        }

        #endregion
    }
}
