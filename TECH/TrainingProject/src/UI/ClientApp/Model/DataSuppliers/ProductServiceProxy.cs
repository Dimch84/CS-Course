using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using ClientApp.Other;
using DTO;

namespace ClientApp.Model.DataSuppliers
{
    class ProductServiceProxy : IDisposable
    {
        private readonly HttpClient _client;

        public ProductServiceProxy()
        {
            _client = new HttpClient();

            string baseUrl = ConfigurationManager.AppSettings["ProductsServiceUrl"];

            Logger.Info($"Product service url: {baseUrl}");

            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public virtual IEnumerable<ProductDTO> GetAllProducts()
        {
            Logger.Info("Service method 'GetAllProducts' is called");

            try
            {
                HttpResponseMessage response = _client.GetAsync("api/products/getallproducts").Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<IEnumerable<ProductDTO>>().Result
                    : null;
            }
            catch (Exception e)
            {
                Logger.Error("ProductService Error", e);
                throw;
            }
        }

        public virtual ProductDTO GetProductById(Guid productId)
        {
            Logger.Info($"Service method 'GetProductById{productId}' is called");

            try
            {
                HttpResponseMessage response = _client.GetAsync($"api/products/GetProductById?productId={productId}").Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<ProductDTO>().Result
                    : null;
            }
            catch (Exception e)
            {
                Logger.Error("ProductService Error", e);
                throw;
            }
        }

        public virtual List<ImageDTO> GetProductImages(Guid productId)
        {
            Logger.Info($"Service method 'GetProductImages{productId}' is called");

            try
            {
                HttpResponseMessage response =
                    _client.GetAsync($"api/products/GetProductImages?productId={productId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<ImageDTO> img = response.Content.ReadAsAsync<IEnumerable<ImageDTO>>().Result;
                    return img.ToList();
                }

                return null;
            }
            catch (Exception e)
            {
                Logger.Error("ProductService Error", e);
                throw;
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
