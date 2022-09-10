using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using DTO;
using SqlHelper;
using DataStorage.Mappers;

namespace DataStorage.DataProviders
{
    public class ImageDataProvider
    {
        public static IList<ImageDTO> GetProductImages(Guid productId)
        {
            string sqlQuery = XmlStrings.GetString(Tables.Images, "GetByProduct");
            SqlParameter param = new SqlParameter("@productId", productId);

            var result = DBHelper.GetData(
                new ImageDTOMapper(),
                sqlQuery, param);

            return result;
        }
    }
}
