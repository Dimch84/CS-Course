using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSamples
{
    public class GenericClassDemo<T>
    {
        public T Display(T value)
        {
            return value;
        }
    }
}
