using Northwind.Service.Models;
using Northwind.Service.Services.Interfaces;
using System.Collections.Generic;

namespace Northwind.Service.Services
{
    public class ProductsService : IProductsService
    {
        public IEnumerable<ProductDTO> GetProducts()
        {
            return new List<ProductDTO>
            {
                new ProductDTO { ProductID = 1 },
            };
        }
    }
}
