using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncDelegates;
using ComponentsMediator;

namespace DelegatesIntro
{
    public delegate int IntegerOperationDelegate(int first, int second);

    class Program
    {
        public delegate void WorldSavedHandler(string saviourName, DateTime dateForNextCatastrophy);
        public delegate string SomeoneJustShoutedHandler(string who, DateTime when);
        public event WorldSavedHandler WorldHasBeenSaved;
        
        static void Main(string[] args)
        {
            // Case 1
            AsynchronousProcessSimulation asyncSimulation = new AsynchronousProcessSimulation();
            asyncSimulation.StartReporting();

            Console.ReadKey();
            Console.WriteLine();

            // Case 2
            ProductChangeInitiator initiator = new ProductChangeInitiator(2);
            // subscribe on events
            Warehouse warehouse = new Warehouse();
            CEO ceo = new CEO();
            ProductChangeSimulation simulation = new ProductChangeSimulation();
            simulation.SimulateProductChange(initiator);

            Console.ReadKey();
            Console.WriteLine();

            // Case 3. Action and Func
            Action<string> actionOfString = MatchingActionOfT;
            Action<string> consoleString = Console.WriteLine;
            actionOfString("Test");
            consoleString("Test from console.");

            Action<int, int> voidAction = (f, s) => Console.WriteLine(f + s);

            IntegerOperationDelegate addIntegersDelegate = (f, s) => { return f + s; };
            IntegerOperationDelegate addSquaresOfIntegersDelegate = (f, s) => (f * f) + (s * s);
            IntegerOperationDelegate doSomethingRandomDelegate = (f, s) => (f + 3) * (s + 4);

            ProcessIntegers processIntegers = new ProcessIntegers();
            processIntegers.DoAction(2, 2, voidAction);
            processIntegers.DoAction(4, 5, addSquaresOfIntegersDelegate);
            processIntegers.DoAction(4, 5, doSomethingRandomDelegate);

            Func<int, int, double> func = MatchingFuncOfT;
            double res = func(3, 2);

            Func<int, int, int> addIntegersFunction = (f, s) => f + s;
            Func<int, int, int> addSquaresOfIntegersFunction = (f, s) => (f * f) + (s * s);
            Func<int, int, int> doSomethingRandomFunction = (f, s) => (f + 3) * (s + 4);

            processIntegers.DoAction(2, 2, addIntegersFunction);
            processIntegers.DoAction(4, 5, addSquaresOfIntegersFunction);
            processIntegers.DoAction(4, 5, doSomethingRandomFunction);

            Console.ReadKey();
            Console.WriteLine();

            // Case 4
            WorldSavedHandler worldSavedHandler = new WorldSavedHandler(WorldSaved);
            WorldSavedHandler worldSavedAgainHandler = new WorldSavedHandler(WorldSavedAgain);
            WorldSavedHandler worldSavedOnceMoreHandler = new WorldSavedHandler(WorldSavedOnceMore);

            SomeoneJustShoutedHandler someoneShoutedHandler = new SomeoneJustShoutedHandler(SomeoneShouted);
            SomeoneJustShoutedHandler someoneShoutedAgainHandler = new SomeoneJustShoutedHandler(SomeoneShoutedAgain);
            SomeoneJustShoutedHandler someoneShoutedOnceMoreHandler = new SomeoneJustShoutedHandler(SomeoneShoutedOnceMore);

            someoneShoutedHandler += someoneShoutedAgainHandler + someoneShoutedOnceMoreHandler;
            string finalShout = someoneShoutedHandler("The neighbour", DateTime.UtcNow);
            Console.WriteLine("Final shout: " + finalShout);

            SaveWorld(worldSavedAgainHandler);

            Console.ReadKey();
            Console.WriteLine();

            // Case 5
            SuperHero superman = new SuperHero();
            superman.WorldSaved += superman_WorldSaved;
            superman.WorldSavingCompleted += superman_WorldSavingCompleted;

            superman.WorldSavingCompleted += delegate (object sender, WorldSavingCompletedEventArgs e)
            {
                StringBuilder superHeroMessageBuilder = new StringBuilder();
                superHeroMessageBuilder.Append("From delegate. Superhero has saved the world! Name: ").Append(e.Saviour)
                    .Append(", time spent saving the world: ").Append(e.HoursItTookToSaveWorld).Append(", message from the superhero: ")
                    .Append(e.MessageFromSaviour);
                Console.WriteLine(superHeroMessageBuilder.ToString());
            };

            superman.WorldSavingCompleted += (s, e) =>
            {
                StringBuilder superHeroMessageBuilder = new StringBuilder();
                superHeroMessageBuilder.Append("From anonymous method. Superhero has saved the world! Name: ").Append(e.Saviour)
                    .Append(", time spent saving the world: ").Append(e.HoursItTookToSaveWorld).Append(", message from the superhero: ")
                    .Append(e.MessageFromSaviour);
                Console.WriteLine(superHeroMessageBuilder.ToString());
            };

            superman.SaveTheWorld("Superman", DateTime.UtcNow.AddMonths(3));

            Console.ReadKey();
        }

        private static double MatchingFuncOfT(int first, int second)
        {
            return first / second;
        }

        private static void MatchingActionOfT(string inputs)
        {
            Console.WriteLine(inputs);
        }

        static void superman_WorldSavingCompleted(object sender, WorldSavingCompletedEventArgs e)
        {
            StringBuilder superHeroMessageBuilder = new StringBuilder();
            superHeroMessageBuilder.Append("Superhero has saved the world! Name: ").Append(e.Saviour)
                .Append(", time spent saving the world: ").Append(e.HoursItTookToSaveWorld).Append(", message from the superhero: ")
                .Append(e.MessageFromSaviour);
            Console.WriteLine(superHeroMessageBuilder.ToString());
        }

        static void superman_WorldSaved(object sender, WorldSavedEventArgs e)
        {
            StringBuilder superHeroMessageBuilder = new StringBuilder();
            superHeroMessageBuilder.Append("Superhero reporting progress! Name: ")
                .Append(e.SaviourName).Append(", has been working for ").Append(e.WorkHasBeenOngoingHs)
                .Append(" hours, ").Append(" date of next occasion: ").Append(e.DateOfNextCatastrophy);
            Console.WriteLine(superHeroMessageBuilder.ToString());
        }

        static string SomeoneShouted(string who, DateTime when)
        {
            return string.Concat(who , " has shout on ", when);
        }

        static string SomeoneShoutedAgain(string who, DateTime when)
        {
            return string.Concat(who, " has shouted on ", when);
        }

        static string SomeoneShoutedOnceMore(string who, DateTime when)
        {
            return string.Concat(who, " has shouted once more on ", when);
        }

        static void SaveWorld(WorldSavedHandler worldSaved)
        {
            worldSaved("Bruce Willis", DateTime.UtcNow.AddMonths(5));
        }

        static void WorldSaved(string saviour, DateTime nextTime)
        {
            Console.WriteLine(string.Concat("World saved by ", saviour, ", get ready again by ", nextTime));
        }

        static void WorldSavedOnceMore(string saviour, DateTime nextTime)
        {
            Console.WriteLine(string.Concat("World saved once again by ", saviour, ", get ready again by ", nextTime));
        }

        static void WorldSavedAgain(string saviour, DateTime nextTime)
        {
            Console.WriteLine(string.Concat("World saved this time by ", saviour, ", get ready again by ", nextTime));
        }
    }
}
