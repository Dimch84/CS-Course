using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProductService.Test
{
    [TestClass]
    public class ProductServiceTest
    {
        private HttpClient _client = createClient();

        private static HttpClient createClient()
        {
            HttpClient client = new HttpClient();

            string baseUrl = ConfigurationManager.AppSettings["ProductsServiceUrl"];

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        [TestMethod]
        public void TestGetAllProducts()
        {
            HttpResponseMessage response =  _client.GetAsync("api/products/getallproducts").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);

            IEnumerable<ProductDTO> products = response.Content.ReadAsAsync<IEnumerable<ProductDTO>>().Result;
            Assert.IsNotNull(products);

            var productList = products.ToList();
            Assert.IsTrue(productList.Count > 0);
            Assert.IsNull(productList.FirstOrDefault(p => p.IconData == null));
        }

        [TestMethod]
        public void TestGetProductById()
        {
            var product = getFirstProduct();

            HttpResponseMessage response = _client.GetAsync($"api/products/GetProductById?productId={product.Id}").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);

            var retProduct = response.Content.ReadAsAsync<ProductDTO>().Result;

            Assert.IsTrue(retProduct != null);
            Assert.IsTrue(retProduct.IconData != null);
        }

        [TestMethod]
        public void TestGetProductImages()
        {
            var product = getFirstProduct();

            HttpResponseMessage response = _client.GetAsync($"api/products/GetProductImages?productId={product.Id}").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);

            IEnumerable<ImageDTO> img = response.Content.ReadAsAsync<IEnumerable<ImageDTO>>().Result;
            Assert.IsNotNull(img);

            var images = img.ToList();
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

        private ProductDTO getFirstProduct()
        {
            HttpResponseMessage response = _client.GetAsync("api/products/getallproducts").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);

            IEnumerable<ProductDTO> products = response.Content.ReadAsAsync<IEnumerable<ProductDTO>>().Result;
            Assert.IsNotNull(products);

            var p = products.FirstOrDefault();
            Assert.IsNotNull(p);

            return p;
        }
    }
}
