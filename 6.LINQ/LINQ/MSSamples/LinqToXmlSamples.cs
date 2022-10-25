using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.IO;

using NUnit.Framework;

namespace SampleQueries
{
    [TestFixture]
    public class LinqToXmlSamples
    {
        private static readonly string dataPath = @"..\..\Data\";

        [Test]
        [Category("Construction")]
        [Description("Construct an XElement from string")]
        public void XLinq1()
        {
            string xml = "<purchaseOrder price='100'>" +
                            "<item price='50'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                          "</purchaseOrder>";
            XElement po = XElement.Parse(xml);
            Console.WriteLine(po);

        }

        [Test]
        [Category("Construction")]
        [Description("Add XML declaration to a document")]
        public void XLinq2()
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-16", "Yes"),
                                          new XElement("foo"));
            StringWriter sw = new StringWriter();
            doc.Save(sw);
            Console.WriteLine(sw);

        }

        [Test]
        [Category("Write")]
        [Description("Write an XElement to XmlWriter using the WriteTo method")]
        public void XLinq3()
        {
            XElement po1 = new XElement("PurchaseOrder",
                            new XElement("Item", "Motor",
                              new XAttribute("price", "100")));

            XElement po2 = new XElement("PurchaseOrder",
                            new XElement("Item", "Cable",
                              new XAttribute("price", "10")));

            XElement po3 = new XElement("PurchaseOrder",
                            new XElement("Item", "Switch",
                              new XAttribute("price", "10")));

            StringWriter sw = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter w = XmlWriter.Create(sw, settings);
            w.WriteStartElement("PurchaseOrders");

            po1.WriteTo(w);
            po2.WriteTo(w);
            po3.WriteTo(w);

            w.WriteEndElement();
            w.Close();
            Console.WriteLine(sw.ToString());
        }

        [Test]
        [Category("Query")]
        [Description("Selects all the CustomerIDs in the xml document")]
        public void XLinq4()
        {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var query = doc.Element("Root")
                           .Elements("Customers")
                           .Attributes("CustomerID");
            foreach (XAttribute result in query)
                Console.WriteLine(result.Name + " = " + result.Value);

        }

        [Test]
        [Category("Query")]
        [Description("Filter query results using where")]
        public void XLinq5()
        {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var query =
                    from
                        c in doc.Element("Root")
                                .Elements("Customers")
                    where
                        c.Element("FullAddress")
                         .Element("Country")
                         .Value == "Germany"
                    select c;

            foreach (XElement result in query)
                Console.WriteLine(result);
        }

        [Test]
        [Category("Query")]
        [Description("Add customer company info to orders of the first customer")]
        public void XLinq6()
        {
            XDocument customers = XDocument.Load(dataPath + "nw_customers.xml");
            XDocument orders = XDocument.Load(dataPath + "nw_orders.xml");

            var query =
                from customer in customers.Descendants("Customers").Take(1)
                join order in orders.Descendants("Orders")
                           on (string)customer.Attribute("CustomerID") equals
                              (string)order.Element("CustomerID")
                select
                    new XElement("Order",
                                order.Nodes(),
                                customer.Element("CompanyName"));

            foreach (var result in query)
                Console.WriteLine(result);

        }

        [Test]
        [Category("Query")]
        [Description("Check if 2 sequences of nodes are equal. " +
                     "Did Serge and peter co-author all of their the books?")]
        public void XLinq7()
        {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            var b1 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                        .Elements("first")
                                        .Any(f => (string)f == "Serge"));
            var b2 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                        .Elements("first")
                                        .Any(f => (string)f == "Peter"));
            bool result = b2.SequenceEqual(b1);
            Console.WriteLine(result);
        }

        [Test]
        [Category("Query")]
        [Description("Find tax on an order")]
        public void XLinq8()
        {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                         "</order>";

            XElement order = XElement.Parse(xml);
            double tax = order.Elements("item")
                              .Aggregate((double)0, Tax);

            Console.WriteLine("The total tax on the order @10% is ${0}", tax);

        }
        static double Tax(double seed, XElement item)
        {
            return seed + (double)item.Attribute("price") * 0.1;
        }

        [Test]
        [Category("Grouping")]
        [Description("Create a directory of customers grouped by country and city")]
        public void XLinq9()
        {
            XDocument customers = XDocument.Load(dataPath + "nw_customers.xml");
            XElement directory =
             new XElement("directory",
                from customer in customers.Descendants("Customers")
                from country in customer.Elements("FullAddress").Elements("Country")
                group customer by (string)country into countryGroup
                let country = countryGroup.Key
                select
                    new XElement("Country",
                                 new XAttribute("name",
                                                country),
                                 new XAttribute("numberOfCustomers",
                                                countryGroup.Count()),
                                 from customer in countryGroup
                                 from city in customer.Descendants("City")
                                 group customer by (string)city into cityGroup
                                 let city = cityGroup.Key
                                 select
                                    new XElement("City",
                                                 new XAttribute("name",
                                                                city),
                                                 new XAttribute("numberOfCustomers",
                                                                cityGroup.Count()),
                                                                cityGroup.Elements("ContactName"))));
            Console.WriteLine(directory);

        }

        [Test]
        [Category("Language Integration")]
        [Description("Find all orders for customers in a List")]
        public void XLinq10()
        {
            XDocument doc = XDocument.Load(dataPath + "nw_orders.xml");
            List<Customer> customers = SomeMethodToGetCustomers();
            var result =
                from customer in customers
                from order in doc.Descendants("Orders")
                where
                    customer.id == (string)order.Element("CustomerID")
                select
                    new { custid = (string)customer.id, orderdate = (string)order.Element("OrderDate") };
            foreach (var tuple in result)
                Console.WriteLine("Customer id = {0}, Order Date = {1}",
                                    tuple.custid, tuple.orderdate);

        }
        class Customer
        {
            public Customer(string id) { this.id = id; }
            public string id;
        }
        static List<Customer> SomeMethodToGetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer("VINET"));
            customers.Add(new Customer("TOMSP"));
            return customers;
        }

        [Test]
        [Category("Events")]
        [Description("Attach event listners to the root element and add/remove child element")]
        public void XLinq11()
        {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                            "<item price='50'>Modem</item>" +
                            "<item price='250'>Monitor</item>" +
                            "<item price='10'>Mouse</item>" +
                         "</order>";
            XDocument order = XDocument.Parse(xml);
            // Add event listners to order
            order.Changing += new EventHandler<XObjectChangeEventArgs>(OnChanging);
            order.Changed += new EventHandler<XObjectChangeEventArgs>(OnChanged);
            // Add a new item to order
            Console.WriteLine("Original Order:");
            Console.WriteLine("{0}", order.ToString(SaveOptions.None));
            order.Root.Add(new XElement("item", new XAttribute("price", 350), "Printer"));
            Console.WriteLine("After Add:");
            Console.WriteLine("{0}", order.ToString(SaveOptions.None));
            order.Root.Element("item").Remove();
            Console.WriteLine("After Remove:");
            Console.WriteLine("{0}", order.ToString(SaveOptions.None));
            // Remove event listners
            order.Changing -= new EventHandler<XObjectChangeEventArgs>(OnChanging);
            order.Changed -= new EventHandler<XObjectChangeEventArgs>(OnChanged);
            // Add another element, events should not be raised
            order.Root.Add(new XElement("item", new XAttribute("price", 350), "Printer"));
        }
        public void OnChanging(object sender, XObjectChangeEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("OnChanging:");
            Console.WriteLine("EventType: {0}", e.ObjectChange);
            Console.WriteLine("Object: {0}", ((XNode)sender).ToString(SaveOptions.None));
        }
        public void OnChanged(object sender, XObjectChangeEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("OnChanged:");
            Console.WriteLine("EventType: {0}", e.ObjectChange);
            Console.WriteLine("Object: {0}", ((XNode)sender).ToString(SaveOptions.None));
            Console.WriteLine();
        }

        [Test]
        [Category("Events")]
        [Description("Log orders as they are added")]
        public void XLinq12()
        {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                            "<item price='50'>Modem</item>" +
                            "<item price='250'>Monitor</item>" +
                            "<item price='10'>Mouse</item>" +
                         "</order>";
            XElement order = XElement.Parse(xml);
            int orderCount = 0;
            StringWriter log = new StringWriter();

            order.Changed += new EventHandler<XObjectChangeEventArgs>(
                    delegate (object sender, XObjectChangeEventArgs e)
                    {
                        orderCount++;
                        log.WriteLine(((XNode)sender).ToString(SaveOptions.None));
                    });

            for (int i = 0; i < 100; i++)
            {
                order.Add(new XElement("item", new XAttribute("price", 350), "Printer"));
            }

            Console.WriteLine("Orders Received: {0}", orderCount);
            Console.WriteLine("Orders Log:");
            Console.WriteLine(log.ToString());
        }
    }
}