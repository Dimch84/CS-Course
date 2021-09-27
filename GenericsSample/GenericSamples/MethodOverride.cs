using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSamples
{
    public class BaseClass<T>
    {
        public virtual T MyMethod(T param)
        {
            Console.WriteLine("Inside BaseClass.BaseMethod()");
            return param;
        }
    }

    public class DerivedClass<T> : BaseClass<T>
    {
        public override T MyMethod(T param)
        {
            Console.WriteLine("Here I'm inside of DerivedClass.DerivedMethod()");
            return param;
        }
    }
}
