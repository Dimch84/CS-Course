using System;

namespace DelegatesIntro
{
	public class WorldSavingCompletedEventArgs : EventArgs
	{
		public string Saviour { get; set; }
		public DateTime TimeForNextCatastrophy { get; set; }
		public int HoursItTookToSaveWorld { get; set; }
		public string MessageFromSaviour { get; set; }
	}
}
