using AutoMapper;
using Northwind.Repository.Entities;
using Northwind.Repository.UnitOfWork;
using Northwind.Service.Models;

namespace Northwind.Api.AutoMappings.Mappers
{
    public class ProductMapper : Profile
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductMapper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            CreateMap<Product, ProductDTO>()
                .ReverseMap();
        }
    }
}