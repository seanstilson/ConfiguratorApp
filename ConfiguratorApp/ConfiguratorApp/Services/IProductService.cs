using ConfiguratorApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfiguratorApp.Services
{
   public interface IProductService
    {
        List<Product> GetAllProducts();

        Product GetProductByRevision(Guid ID, string revision);
    }
}
