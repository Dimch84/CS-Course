namespace WarriorFactory.Interfaces
{
    public interface IUnitFactory
    {
        IUnit CreateUnit(string unitType);
    }
}