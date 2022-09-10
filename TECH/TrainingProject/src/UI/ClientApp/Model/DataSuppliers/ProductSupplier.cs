using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using ClientApp.Other;

namespace ClientApp.Model.DataSuppliers
{
    class ProductSupplier : IProductSupplier
    {
        private readonly ProductServiceProxy _service ;

        public ProductSupplier(ProductServiceProxy service)
        {
            _service = service;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            foreach (var productDTO in _service.GetAllProducts())
            {
                var p = Product.CreateProduct(
                    productDTO.Id, 
                    productDTO.Name,
                    productDTO.Description,
                    Helper.LoadImage(productDTO.IconData),
                    null);

                products.Add(p);
            }

            return products;
        }

        public Product GetProductById(Guid productId)
        {
            var productDTO = _service.GetProductById(productId);
            var imagesDTO = _service.GetProductImages(productId);

            var images = new List<BitmapImage>();
            foreach (var i in imagesDTO)
            {
                images.Add(Helper.LoadImage(i.Data));
            }

            return Product.CreateProduct(
                    productDTO.Id,
                    productDTO.Name,
                    productDTO.Description,
                    Helper.LoadImage(productDTO.IconData),
                    images);
        }
    }
}
