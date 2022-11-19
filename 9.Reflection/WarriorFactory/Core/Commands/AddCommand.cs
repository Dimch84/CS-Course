﻿namespace WarriorFactory.Core.Commands
{
    using Interfaces;

    public class AddCommand : Command
    {
        public AddCommand(string[] data, IRepository repository, IUnitFactory unitFactory) 
            : base(data, repository, unitFactory)
        {
        }

        public override string Execute()
        {
            string unitType = this.Data[1];
            IUnit unit = this.UnitFactory.CreateUnit(unitType);
            this.Repository.AddUnit(unit);
            string output = $"{unitType} added!";
            return output;
        }
    }
}