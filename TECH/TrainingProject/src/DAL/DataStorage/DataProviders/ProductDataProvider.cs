using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataStorage.Mappers;
using DTO;
using SqlHelper;

namespace DataStorage.DataProviders
{
    public class ProductDataProvider
    {
        public static IList<ProductDTO> GetAllProducts()
        {
            string sqlQuery = XmlStrings.GetString(Tables.Products, "GetAllProducts");

            var result = DBHelper.GetData(
                new ProductDTOMapper(),
                sqlQuery);

            return result;
        }

        public static ProductDTO GetProductById(Guid productId)
        {
            string sqlQuery = XmlStrings.GetString(Tables.Products, "GetProductById");
            SqlParameter param = new SqlParameter("@productId", productId);

            var result = DBHelper.GetItem(
                new ProductDTOMapper(),
                sqlQuery, param);

            return result;
        }
    }
}
