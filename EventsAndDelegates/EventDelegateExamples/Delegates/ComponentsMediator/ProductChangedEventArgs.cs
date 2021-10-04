using System;

namespace ComponentsMediator
{
	public class ProductChangedEventArgs : EventArgs
	{
		public Product Product { get; set; }
	}
}
