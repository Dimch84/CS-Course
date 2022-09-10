using System;
using System.Linq;
using DataStorage.DataProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStorage.Test
{
    [TestClass]
    public class ProductDataProviderTest
    {
        [TestMethod]
        public void TestGetAllProducts()
        {
            var products = ProductDataProvider.GetAllProducts();

            Assert.IsTrue(products != null && products.Count > 0);
            Assert.IsNull(products.FirstOrDefault(p => p.IconData == null));
        }

        [TestMethod]
        public void TestGetProductById()
        {
            //Guid prodId = new Guid("9ADA27D1-2831-E911-8791-408D5C760934");

            var products = ProductDataProvider.GetAllProducts();
            Assert.IsTrue(products != null && products.Count > 0);

            Guid prodId = products[0].Id;
            var product = ProductDataProvider.GetProductById(prodId);

            Assert.IsTrue(product != null);
            Assert.IsTrue(product.IconData != null);
        }
    }
}
