namespace CollectionHierarchy.Controllers
{
    using Interfaces;
    using Models;
    using System;
    using System.Text;

    public class Engine
    {
        private IAddCollection<string> addcollection;
        private IAddRemoveCollection<string> addRemoveCollection;
        private IMyList<string> myList;
        private StringBuilder resultingOutput;

        public Engine()
        {
            this.addcollection = new AddCollection<string>();
            this.addRemoveCollection = new AddRemoveCollection<string>();
            this.myList = new MyList<string>();
            this.resultingOutput = new StringBuilder();
        }

        public void Run()
        {
            var input = "One 2 Три IV 5 Six Семь Eight".Split();

            this.FillCollection(input, this.addcollection);
            this.FillCollection(input, this.addRemoveCollection);
            this.FillCollection(input, this.myList);

            this.resultingOutput.AppendLine();

            var numberOfRemovals = 6;
            this.RemoveOperation(numberOfRemovals, this.addRemoveCollection);
            this.RemoveOperation(numberOfRemovals, this.myList);

            Console.WriteLine(this.resultingOutput.ToString().Trim());
        }

        private void FillCollection(string[] input, IAddCollection<string> collection)
        {
            foreach (var str in input)
            {
                var index = collection.Add(str);
                this.resultingOutput.Append($"{index} ");
            }

            this.resultingOutput
                .Remove(this.resultingOutput.Length - 1, 1)
                .AppendLine();
        }

        private void RemoveOperation<T>(int numberOfRemovals, IAddRemoveCollection<T> collection)
        {
            while (numberOfRemovals > 0)
            {
                var removedElement = collection.Remove();
                this.resultingOutput.Append($"{removedElement} ");
                numberOfRemovals--;
            }

            this.resultingOutput
                .Remove(this.resultingOutput.Length - 1, 1)
                .AppendLine();
        }
    }
}
