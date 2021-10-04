using System;

namespace ComposedMission
{
    public interface IMission {

        event EventHandler<double> OnCalculate;

        String Name { get; }

        String Type { get; }

        double Calculate(double value);
    }
}
