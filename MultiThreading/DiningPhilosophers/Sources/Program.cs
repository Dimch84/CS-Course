
using System;
using Common;

namespace DiningPhilosophers
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DiningPhilosophersEmulator emulator = new DiningPhilosophersEmulator(3, ELockAlgo.EBasic);
                emulator.Run();

                DiningPhilosophersEmulator emulator2 = new DiningPhilosophersEmulator(3, ELockAlgo.ELockLevel);
                emulator2.Run();

                DiningPhilosophersEmulator emulator3 = new DiningPhilosophersEmulator(3, ELockAlgo.ETryWaiting);
                emulator3.Run();
            }
            catch(Exception exc)
            { 
                CustomLogger.LogMessage(exc.Message);
            }
            Console.ReadKey();
        }
    }
}  
   
 