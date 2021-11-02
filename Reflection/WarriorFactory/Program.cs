namespace WarriorFactory
{
    using Interfaces;
    using Core;
    using Core.Factories;
    using Data;

    public class Program
    {
        public static void Main()
        {
            IRepository repository = new UnitRepository();
            IUnitFactory unitFactory = new UnitFactory();
            IRunnable engine = new Engine(repository, unitFactory);
            engine.Run();

            // Example:
            // add Archer
        }
    }
}