using System;
using System.Data.SqlClient;
using DTO;

namespace DataStorage.Mappers
{
    class ImageDTOMapper : IMapper<ImageDTO>
    {
        public ImageDTO ReadItem(SqlDataReader rd)
        {
            return new ImageDTO
            {
                Id = (Guid)rd["ImageId"],
                Data = (Byte[])rd["Data"]
            };
        }
    }
}
