using System;
using System.Collections.Generic;
using System.Web.Http;
using DataStorage.DataProviders;
using DTO;

namespace ProductsService.Controllers
{
    /// <summary>
    /// Controller to operate with products
    /// </summary>
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Returns test string
        /// </summary>
        /// <returns></returns>
        public string GetTestString()
        {
            return "Hi There!";
        }

        /// <summary>
        /// Returns all available products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return ProductDataProvider.GetAllProducts();
        }

        /// <summary>
        /// Returns product data without images
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductDTO GetProductById(Guid productId)
        {
            return ProductDataProvider.GetProductById(productId);
        }

        /// <summary>
        /// Returns all images of the product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IEnumerable<ImageDTO> GetProductImages(Guid productId)
        {
            return ImageDataProvider.GetProductImages(productId);
        }
    }
}
