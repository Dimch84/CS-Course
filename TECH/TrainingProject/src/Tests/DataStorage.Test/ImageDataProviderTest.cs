using System;
using System.IO;
using System.Linq;
using DataStorage.DataProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStorage.Test
{
    [TestClass]
    public class ImageDataProviderTest
    {
        [TestMethod]
        public void TestGetProductImages()
        {
            //Guid prodId = new Guid("9ADA27D1-2831-E911-8791-408D5C760934");

            var products = ProductDataProvider.GetAllProducts();
            Assert.IsTrue(products != null && products.Count > 0);

            Guid prodId = products[0].Id;

            var images = ImageDataProvider.GetProductImages(prodId);

            Assert.IsTrue(images != null && images.Count > 0);
            Assert.IsNull(images.FirstOrDefault(i => i.Data == null));

            // check image data is correct
            const string imgFileName = "TestImg.jpg";
            if (File.Exists(imgFileName))
                File.Delete(imgFileName);

            using (FileStream fs = new FileStream(imgFileName, FileMode.CreateNew))
            {
                fs.Write(images[0].Data, 0, images[0].Data.Length);
            }

            Assert.IsTrue(File.Exists(imgFileName));
        }
    }
}
