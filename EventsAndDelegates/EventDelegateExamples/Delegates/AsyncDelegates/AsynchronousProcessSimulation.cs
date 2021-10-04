using System;
using System.Threading;

namespace AsyncDelegates
{
	public class AsynchronousProcessSimulation
	{
		private delegate void ShowProgressDelegate(int status);

		public void StartReporting()
		{
			ShowProgressDelegate progressDelegate = new ShowProgressDelegate(DoProgress);

			progressDelegate.BeginInvoke(10, null, null);
			Console.WriteLine("Finishing the StartReporting method.");
		}

		private void DoProgress(int maxValue)
		{
			for (int i = 0; i <= maxValue; i++)
			{
				Console.WriteLine("Time : {0}", i);
				Thread.Sleep(50);
			}
		}
	}
}
