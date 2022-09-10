using System;

namespace DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Byte[] IconData { get; set; }
    }
}
