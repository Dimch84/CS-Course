using System;
using System.Collections.Generic;

namespace AlgTasks.StackQueue
{
    /// <summary>
    /// Implementation of stack set
    /// List of stacks with a given max capacity
    /// </summary>
    /// <typeparam name="T">Content type</typeparam>
    public class StackSet<T>
    {
        private readonly List<Stack<T>> _stackSet;
        private readonly int _maxStackCapacity;

        public StackSet(int maxStackCapacity)
        {
            this._maxStackCapacity = maxStackCapacity;
            this._stackSet = new List<Stack<T>>();

            this._stackSet.Add(new Stack<T>());
        }

        public void Push(T value)
        {
            var currentStack = this.GetCurrentStack();
            if (currentStack.Count >= this._maxStackCapacity)
            {
                var newStack = new Stack<T>();
                newStack.Push(value);
                this._stackSet.Add(newStack);
            }
            else
            {
                currentStack.Push(value);
            }
        }

        public T Pop()
        {
            var currentStack = this.GetCurrentStack();
            return this.PopFromStack(currentStack, this._stackSet.Count - 1);
        }

        public T PopAt(int stackIndex)
        {
            if (stackIndex >= this._stackSet.Count)
            {
                throw new OverflowException();
            }

            var stack = this._stackSet[stackIndex];
            return this.PopFromStack(stack, stackIndex);
        }

        private T PopFromStack(Stack<T> stack, int stackIndex)
        {
            var value = stack.Pop();
            if (stack.Count == 0 && this._stackSet.Count > 1)
            {
                this._stackSet.RemoveAt(stackIndex);
            }

            return value;
        }

        private Stack<T> GetCurrentStack()
        {
            return this._stackSet[this._stackSet.Count - 1];
        }
    }
}
