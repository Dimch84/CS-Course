using System;
using System.Collections.Generic;

namespace ComposedMission
{
    public class ComposedMission : IMission {

        private LinkedList<CalcDelegate> calcs = new LinkedList<CalcDelegate>();

        public ComposedMission(string name) {
            this.Type = "Composed";
            this.Name = name;
        }

        public string Name { get; }

        public string Type { get; }

        public ComposedMission Add(CalcDelegate calc) {
            calcs.AddLast(calc);
            return this;
        }

        public event EventHandler<double> OnCalculate;

        public double Calculate(double val) {
            foreach (CalcDelegate c in calcs) {
                val = c(val);
            }

            OnCalculate?.Invoke(this, val);
            return val;
        }
    }
}