using Northwind.Repository.Entities;
using Northwind.Repository.Models;
using System.Collections.Generic;

namespace Northwind.Repository.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IEnumerable<Product> Search(ProductSearchModel searchModel);
    }
}
