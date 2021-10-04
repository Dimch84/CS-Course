using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegatesIntro
{
	public class ProductService
	{
		private List<Product> _allProducts = new List<Product>()
		{
			new Product(){Description = "FirstProduct", Id = 1, Name = "FP", OnStock = 456}
			, new Product(){Description = "SecondProduct", Id = 2, Name = "SP", OnStock = 123}
			, new Product(){Description = "ThirdProduct", Id = 3, Name = "TP", OnStock = 987}
			, new Product(){Description = "FourthProduct", Id = 4, Name = "FoP", OnStock = 432}
			, new Product(){Description = "FifthProduct", Id = 5, Name = "FiP", OnStock = 745}
			, new Product(){Description = "SixthProduct", Id = 6, Name = "SiP", OnStock = 456}
		};

		public void PlayWithLinq()
		{
			IEnumerable<Product> filteredProducts = _allProducts.Where(p => p.OnStock > 300 && p.Id < 4).OrderBy(p => p.Name);
			foreach (Product prod in filteredProducts)
			{
				Console.WriteLine(prod.Id);
			}
		}
	}
}
