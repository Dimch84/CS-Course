using System;

namespace DelegatesIntro
{
    public class ProcessIntegers
    {
        public void DoAction(int firstInteger, int secondInteger, IntegerOperationDelegate del)
        {
            int result = del(firstInteger, secondInteger);
            Console.WriteLine(result);
        }

        public void DoAction(int firstInteger, int secondInteger, Action<int, int> action)
        {
            action(firstInteger, secondInteger);
            Console.WriteLine("Paramaters passed: {0}, {1}", firstInteger, secondInteger);
        }

        public void DoAction(int firstInteger, int secondInteger, Func<int, int, int> action)
        {
            int res = action(firstInteger, secondInteger);
            Console.WriteLine("Func result: {0}", res);
        }
    }
}
