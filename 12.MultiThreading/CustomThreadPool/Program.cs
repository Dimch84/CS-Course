using System;
using System.Threading;

namespace _10.CustomThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomThreadPool.QueueUserWorkItem(Console.WriteLine, "My Test");
            CustomThreadPool.QueueUserWorkItem(Console.WriteLine, 123);
            CustomThreadPool.QueueUserWorkItem(Console.WriteLine, DateTime.Now);

            Thread.Sleep(TimeSpan.FromSeconds(1));

            CustomThreadPool.QueueUserWorkItem((arg) =>
            {
                string s = (string)arg; char[] charArray = s.ToCharArray();
                Array.Reverse(charArray);
                Console.WriteLine(new string(charArray));
            }, "New text string here");

            CustomThreadPool.CleanUp();
        }
    }
}
