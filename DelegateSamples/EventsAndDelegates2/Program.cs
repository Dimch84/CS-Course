using System;
using System.Collections.Generic;
using System.Text;

namespace EventsAndDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person("John", "Doe", 35);
            //p.WorkPerformed += p_WorkPerformed;

            //Using a lambda
            p.WorkPerformed += (s, e) =>
                {
                    Console.WriteLine("Employee performed " +
                    e.Hours.ToString() + " hours of work on " +
                    e.WorkDate.ToShortDateString());
                };

            p.DoWork(25, 4);
            Console.ReadLine();
        }

        //public static void p_WorkPerformed(object sender,
        //    WorkPerformedEventArgs e)
        //{
        //    Console.WriteLine("Employee performed " +
        //    e.Hours.ToString() + " hours of work on " +
        //    e.WorkDate.ToShortDateString());
        //}
    }
}
