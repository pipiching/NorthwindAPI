using AutoMapper;
using Northwind.Api.AutoMappings.Mappers;
using Northwind.Repository.UnitOfWork;

namespace Northwind.Api.AutoMappings
{
    public class MappingConfiguration
    {
        public static IMapper CreateMapper(IUnitOfWork unitOfWork)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMapper(unitOfWork));
            });

            return mappingConfig.CreateMapper();
        }
    }
}