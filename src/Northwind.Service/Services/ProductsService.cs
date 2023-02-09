using AutoMapper;
using Northwind.Common.Enums;
using Northwind.Common.Utilities;
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

        public void Create(Product product)
        {
            _unitOfWork.ProductRepository.Insert(product);
        }

        public ProductDTO Get(int productID)
        {
            Product product = _unitOfWork.ProductRepository.Get(productID);
            if (product == null)
            {
                throw new OperationalException(
                    ErrorType.INSTANCE_NOT_FOUND,
                    $"Couldn't find product: {productID}");
            }
            ProductDTO productDTO = _mapper.Map<Product, ProductDTO>(product);

            return productDTO;
        }

        public IEnumerable<ProductDTO> Get(ProductSearchModel searchModel)
        {
            IEnumerable<Product> products = _unitOfWork.ProductRepository.Search(searchModel);
            IEnumerable<ProductDTO> productDTOs = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);

            return productDTOs;
        }

        public void Update(Product product)
        {
            if (_unitOfWork.ProductRepository.Get(product.ProductID) == null)
            {
                throw new OperationalException(
                    ErrorType.INSTANCE_NOT_FOUND,
                    $"Couldn't find product: {product.ProductID}");
            }
            _unitOfWork.ProductRepository.Update(product);
        }

        public void Delete(int productID)
        {
            Product product = _unitOfWork.ProductRepository.Get(productID);
            if (product == null)
            {
                throw new OperationalException(
                    ErrorType.INSTANCE_NOT_FOUND,
                    $"Couldn't find product: {productID}");
            }

            _unitOfWork.ProductRepository.Delete(product);
        }
    }
}
