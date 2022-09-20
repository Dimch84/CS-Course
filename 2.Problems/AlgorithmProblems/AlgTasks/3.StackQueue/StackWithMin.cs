using System.Collections.Generic;

namespace AlgTasks.StackQueue
{
    public class StackWithMin : Stack<int>
    {
        private readonly Stack<int> _stackOfMins;

        public StackWithMin() : base()
        {
            this._stackOfMins = new Stack<int>();
        }

        public int Min => this._stackOfMins.Count == 0
            ? int.MaxValue
            : _stackOfMins.Peek();

        public new void Push(int value)
        {
            if (value <= this.Min)
            {
                this._stackOfMins.Push(value);
            }

            base.Push(value);
        }

        public new int Pop()
        {
            var value = base.Pop();
            if (value == this.Min)
            {
                this._stackOfMins.Pop();
            }

            return value;
        }
    }
}
