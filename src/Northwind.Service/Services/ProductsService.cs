using AutoMapper;
using Northwind.Repository.Entities;
using Northwind.Repository.Models;
using Northwind.Repository.UnitOfWork;
using Northwind.Service.Models;
using Northwind.Service.Services.Interfaces;
using System.Collections.Generic;

namespace Northwind.Service.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<ProductDTO> GetProducts(ProductSearchModel searchModel)
        {
            IEnumerable<Product> products = _unitOfWork.ProductRepository.Search(searchModel);
            IEnumerable<ProductDTO> productDTOs = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);

            return productDTOs;
        }
    }
}
