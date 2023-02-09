using Northwind.Repository.Entities;
using Northwind.Repository.Models;
using Northwind.Repository.UnitOfWork;
using Northwind.Service.Models;
using Northwind.Service.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Service.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ProductDTO> GetProducts(ProductSearchModel searchModel)
        {
            IEnumerable<Product> products = _unitOfWork.ProductRepository.Search(searchModel);

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
