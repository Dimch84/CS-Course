using System;
using System.Runtime.CompilerServices;

namespace DelegatesIntro
{
    public class EventAccessorExample
    {
        public delegate void WorldSavedHandler(string saviourName, DateTime dateForNextCatastrophy);
        
        private WorldSavedHandler WorldHasBeenSaved;
        public event WorldSavedHandler WorldSaved
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                WorldHasBeenSaved = (WorldSavedHandler)Delegate.Combine(WorldHasBeenSaved, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                WorldHasBeenSaved = (WorldSavedHandler)Delegate.Remove(WorldHasBeenSaved, value);
            }
        }
    }
}
