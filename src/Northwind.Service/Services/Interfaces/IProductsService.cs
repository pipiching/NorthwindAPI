using Northwind.Service.Models;
using System.Collections.Generic;

namespace Northwind.Service.Services.Interfaces
{
    public interface IProductsService
    {
        IEnumerable<ProductDTO> GetProducts();
    }
}
