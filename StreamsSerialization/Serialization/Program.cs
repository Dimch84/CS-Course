using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using DataSerialization.DeserializationCallback;
using System.Runtime.Serialization;

namespace DataSerialization
{
    class Program
    {
        public static void Main()
        {
            DeserializationCallbackDemo();

            XmlSerializerDemo();

            IXmlSerializableDemo();

            BinaryFormatterDemo();

            SoapFormatterDemo();
        }

        private static void DeserializationCallbackDemo()
        {
            Serialize();
            Deserialize();
            Console.ReadLine();
        }

        private static void Serialize()
        {
            Circle c = new Circle(10);
            Console.WriteLine("Object being serialized: " + c.ToString());

            // To serialize the Circle, you must first open a stream for 
            // writing. Use a file stream here.
            FileStream fs = new FileStream("../../Data/DataFile.dat", FileMode.Create);

            // Construct a BinaryFormatter and use it 
            // to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, c);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        private static void Deserialize()
        {
            // Declare the Circle reference.
            Circle c = null;

            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream("../../Data/DataFile.dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the Circle from the file and 
                // assign the reference to the local variable.
                c = (Circle)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            // To prove that the Circle deserialized correctly, display its area.
            Console.WriteLine("Object being deserialized: " + c.ToString());
        }

        private static void XmlSerializerDemo()
        {
            string path = "../../Data/Customer.xml";

            Customer cust = new Customer();
            cust.FirstName = "John";
            cust.LastName = "Doe";

            List<string> numbers = new List<string>();
            numbers.Add("123-123-1234");
            numbers.Add("321-654-5876");

            cust.PhoneNumbers = numbers;

            Console.WriteLine("Serializing Customer object to XML!");
            XmlSerializer s = new XmlSerializer(typeof(Customer));
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                s.Serialize(fs, cust);

            Console.WriteLine("Customer object serialized to XML!");

            Console.WriteLine("Customer being deserialized");

            using (XmlReader reader = XmlReader.Create(path))
            {
                if (s.CanDeserialize(reader))
                {
                    Customer custFromFile = (Customer)s.Deserialize(reader);
                    Console.WriteLine("Customer name: " + custFromFile.FirstName + " " + custFromFile.LastName);
                }
            }

            Console.ReadLine();
        }

        private static void IXmlSerializableDemo()
        {
            string path = "../../Data/Car.xml";

            Car c = new Car();
            c.Make = "VW";
            c.Model = "Touareg";
            c.Year = 2021;

            Console.WriteLine("Serializing Car to XML using IXmlSerializable interface members");
            XmlSerializer s = new XmlSerializer(typeof(Car));
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                s.Serialize(fs, c);
            Console.WriteLine("Car serialized!");

            Console.WriteLine("Deserializing Car using IXmlSerializable interface memebers");
            using (FileStream fs2 = new FileStream(path, FileMode.Open))
            {
                Car c2 = (Car)s.Deserialize(fs2);
                Console.WriteLine("Car deserialized: " + c2.Make + " " + c2.Model);
            }

            Console.ReadLine();
        }

        private static void BinaryFormatterDemo()
        {
            string path = "../../Data/Customer.bin";

            Customer cust = new Customer();
            cust.FirstName = "John";
            cust.LastName = "Doe";

            List<string> numbers = new List<string>();
            numbers.Add("123-123-1234");
            numbers.Add("321-654-5876");
            cust.PhoneNumbers = numbers;

            Console.WriteLine("Serializing Customer object in binary format!");
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                bf.Serialize(fs, cust);

            Console.WriteLine("Customer object serialized!");

            Console.WriteLine("Customer being deserialized");
            using (FileStream fs2 = new FileStream(path, FileMode.Open))
            {
                Customer custFromFile = (Customer)bf.Deserialize(fs2);
                Console.WriteLine("Customer name: " + custFromFile.FirstName + " " + custFromFile.LastName);
            }

            Console.ReadLine();
        }

        // now is legacy format
        private static void SoapFormatterDemo()
        {
            string path = "../../Data/Customer.soap";
            Customer cust = new Customer();
            cust.FirstName = "John";
            cust.LastName = "Doe";

            //SoapFormatter doesn't support generics!
            //List<string> numbers = new List<string>();
            //numbers.Add("123-123-1234");
            //numbers.Add("321-654-5876");
            //cust.PhoneNumbers = numbers;

            Console.WriteLine("Serializing Customer object in SOAP format!");

            SoapFormatter sf = new SoapFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                sf.Serialize(fs, cust);

            Console.WriteLine("Customer object serialized!");
            
            Console.WriteLine("Customer being deserialized");

            using (FileStream fs2 = new FileStream(path, FileMode.Open))
            {
                Customer custFromFile = (Customer)sf.Deserialize(fs2);
                Console.WriteLine("Customer name: " + cust.FirstName + " " + cust.LastName);
            }

            Console.Read();
        }
    }
}
