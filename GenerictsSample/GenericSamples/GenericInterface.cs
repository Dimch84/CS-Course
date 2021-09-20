using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSamples
{
    public interface GenericInterface<T>
    {
        //A generic method
        T GenericMethod(T param);
        //A non-generic method
        void NonGenericMethod();

    }
    //Implementing the interface
    public class ConcreteClass<T> : GenericInterface<T>
    {
        //Implementing interface method
        public T GenericMethod(T param)
        {
            return param;
        }

        public void NonGenericMethod()
        {
            Console.WriteLine("Implementing NonGenericMethod of GenericInterface<T>");
        }
    }

    #region For Q&A and analysis section
    // class ConcreteClass2 : GenericInterface<T>//Error
    //class ConcreteClass2<U> : GenericInterface<T>//Error
    //class ConcreteClass2<U,T> : GenericInterface<T> //valid
    public class ConcreteClass2<T, U> : GenericInterface<T> //also valid
    {
        public T GenericMethod(T param)
        {
            throw new NotImplementedException();
        }

        public void NonGenericMethod()
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
