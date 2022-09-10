using System;
using System.Data.SqlClient;
using DTO;

namespace DataStorage.Mappers
{
    class ProductDTOMapper : IMapper<ProductDTO>
    {
        public ProductDTO ReadItem(SqlDataReader rd)
        {
            return new ProductDTO
            {
                Id = (Guid)rd["ProductId"],
                Name = (string)rd["Name"],
                Description = (string)rd["Description"],
                IconData = (Byte[])rd["Icon"]
            };
        }
    }
}
