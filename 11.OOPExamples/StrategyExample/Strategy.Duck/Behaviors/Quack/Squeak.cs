using System;

namespace Strategy.Duck
{
    /// <summary>
    /// Squeak
    /// </summary>
    public class Squeak : IQuackBehavior
    {
        public string Quacking()
        {
            return "Squeak";
        }
    }
}
