using System.Collections.Generic;

namespace AlgTasks.StackQueue
{
    /// <summary>
    /// Implementation of queue based on 2 stacks
    /// </summary>
    /// <typeparam name="T">Content type</typeparam>
    public class QueueBasedOnStacks<T>
    {
        private readonly Stack<T> _inputStack;
        private readonly Stack<T> _outputStack;

        public QueueBasedOnStacks()
        {
            this._inputStack = new Stack<T>();
            this._outputStack = new Stack<T>();
        }

        public void Enqueue(T value)
        {
            this._inputStack.Push(value);
        }

        public T Dequeue()
        {
            if (this._outputStack.Count == 0)
            {
                this.MoveInputToOutput();
            }

            return this._outputStack.Pop();
        }

        private void MoveInputToOutput()
        {
            while (this._inputStack.Count > 0)
            {
                this._outputStack.Push(this._inputStack.Pop());
            }
        }
    }
}
