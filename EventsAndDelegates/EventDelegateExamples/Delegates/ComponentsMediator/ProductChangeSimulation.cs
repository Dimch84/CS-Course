using System;
using System.Collections.Generic;
using System.Linq;

namespace ComponentsMediator
{
	public class ProductChangeSimulation
	{
		private List<Product> _allProducts = new List<Product>()
		{
			new Product(){Name = "FirstProduct", Id = 1, Registered = DateTime.Now.AddDays(-1), OnStock = 456}
			, new Product(){Name = "SecondProduct", Id = 2, Registered = DateTime.Now.AddDays(-2), OnStock = 123}
			, new Product(){Name = "ThirdProduct", Id = 3, Registered = DateTime.Now.AddDays(-3), OnStock = 987}
			, new Product(){Name = "FourthProduct", Id = 4, Registered = DateTime.Now.AddDays(-4), OnStock = 432}
			, new Product(){Name = "FifthProduct", Id = 5, Registered = DateTime.Now.AddDays(-5), OnStock = 745}
			, new Product(){Name = "SixthProduct", Id = 6, Registered = DateTime.Now.AddDays(-6), OnStock = 456}
		};

		public void SimulateProductChange(ProductChangeInitiator changeInitiator)
		{
			Product selectedProduct = (from p in _allProducts where p.Id == changeInitiator.SelectedProductId select p).FirstOrDefault();
			Mediator.Instance.OnProductChanged(changeInitiator, selectedProduct);
		}
	}
}
