using System;

namespace ComposedMission
{
    public class SingleMission : IMission {
        private CalcDelegate CalcDel;

        public SingleMission(CalcDelegate calcDel, string name) {
            this.Type = "Single";
            this.Name = name;
            this.CalcDel = calcDel;
        }

        public string Name { get; }

        public string Type { get; }

        public event EventHandler<double> OnCalculate;

        public double Calculate(double val) {
            val = CalcDel(val);
            OnCalculate?.Invoke(this, val);
            return val;
        }
    }
}