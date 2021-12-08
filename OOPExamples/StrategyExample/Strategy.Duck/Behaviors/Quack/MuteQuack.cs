using System;

namespace Strategy.Duck
{
    /// <summary>
    /// MuteQuack
    /// </summary>
    public class MuteQuack : IQuackBehavior
    {
        public string Quacking()
        {
            return "<<silence>>";
        }
    }
}
