namespace ExplicitInterfaces
{
    using Interfaces;
    using Models;
    using System;

    public class Program
    {
        public static void Main()
        {
            var human = new Citizen("Joe");

            Console.WriteLine("Type casting:");
            Console.WriteLine(((IPerson)human).GetName());
            Console.WriteLine(((IResident)human).GetName());

            Console.WriteLine();

            Console.WriteLine("Call by interface:");
            IPerson person = human;
            Console.WriteLine(person.GetName());

            IResident resident = human;
            Console.WriteLine(resident.GetName());
        }
    }
}
