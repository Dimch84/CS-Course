using System;
using System.Collections;
using System.Threading;

namespace GcNotifications
{
    class SimpleCase
    {
        private static Boolean s_carryOn;

        private static void Main()
        {
            ArrayList data = new ArrayList();
            s_carryOn = true;

            GC.RegisterForFullGCNotification(10, 10);
            Thread t = new Thread(new ThreadStart(CheckTheGC));
            t.Start();

            Int32 secondsPassed = 0;
            while (s_carryOn)
            {
                Console.WriteLine("{0} seconds passed", secondsPassed++);

                for(Int32 i = 0; i < 1000; i++)
                    data.Add(new Byte[1000]);
                Thread.Sleep(1000);
            }

            GC.CancelFullGCNotification();

            Console.ReadKey();
        }

        private static void CheckTheGC()
        {
            while (true) // Wait for an Approaching Full GC
            {
                GCNotificationStatus s = GC.WaitForFullGCApproach();
                if (s == GCNotificationStatus.Succeeded)
                {
                    Console.WriteLine("Full GC Nears");
                    break;
                }
            }

            while (true) // Wait until the Full GC has finished
            {
                GCNotificationStatus s = GC.WaitForFullGCComplete();
                if (s == GCNotificationStatus.Succeeded)
                {
                    Console.WriteLine("Full GC Complete");
                    break;
                }
            }

            s_carryOn = false;
        }
    }
}
