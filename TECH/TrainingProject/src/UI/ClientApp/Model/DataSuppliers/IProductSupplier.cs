using System;
using System.Collections.Generic;

namespace ClientApp.Model.DataSuppliers
{
    public interface IProductSupplier
    {
        List<Product> GetAllProducts();

        Product GetProductById(Guid productId);
    }
}
