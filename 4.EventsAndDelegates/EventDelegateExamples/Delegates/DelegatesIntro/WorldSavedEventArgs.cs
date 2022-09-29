using System;

namespace DelegatesIntro
{
    public class WorldSavedEventArgs : EventArgs
    {
        public string SaviourName { get; set; }
        public DateTime DateOfNextCatastrophy { get; set; }
        public int WorkHasBeenOngoingHs { get; set; }
    }
}
