using System;
using Microsoft.Owin.Hosting;

namespace ConsoleHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            // test from browser as:
            // http://localhost:31425/api/products/getallproducts

            // help page:
            // http://localhost:31425/help

            const string baseAddress = "http://localhost:31425/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"WebAPI SelfHost started at {baseAddress}");
                Console.WriteLine("Press enter to finish");
                Console.ReadLine();
            }
        }
    }
}
