using Northwind.Repository.Entities;
using Northwind.Repository.Models;
using Northwind.Service.Models;
using System.Collections.Generic;

namespace Northwind.Service.Services.Interfaces
{
    public interface IProductsService
    {
        IEnumerable<ProductDTO> Get(ProductSearchModel searchModel);

        ProductDTO Get(int productID);

        void Create(Product product);

        void Update(Product product);

        void Delete(int productID);
    }
}
