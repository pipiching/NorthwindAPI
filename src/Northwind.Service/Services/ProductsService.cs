using Northwind.Repository.Entities;
using Northwind.Repository.Models;
using Northwind.Repository.Repositories;
using Northwind.Repository.Repositories.Interfaces;
using Northwind.Service.Models;
using Northwind.Service.Services.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Northwind.Service.Services
{
    public class ProductsService : IProductsService
    {
        public ProductsService()
        {

        }

        public IEnumerable<ProductDTO> GetProducts(ProductSearchModel searchModel)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["northwind"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                IProductRepository productRepository = new ProductRepository(connection);
                IEnumerable<Product> products = productRepository.Search(searchModel);

                IEnumerable<ProductDTO> productDTOs = products.Select(s => new ProductDTO
                {
                    ProductID = s.ProductID,
                    ProductName = s.ProductName,
                    SupplierID = s.SupplierID,
                    CategoryID = s.CategoryID,
                    QuantityPerUnit = s.QuantityPerUnit,
                    UnitPrice = s.UnitPrice,
                    UnitsInStock = s.UnitsInStock,
                    UnitsOnOrder = s.UnitsOnOrder,
                    ReorderLevel = s.ReorderLevel,
                    Discontinued = s.Discontinued
                });

                return productDTOs;
            }
        }
    }
}
