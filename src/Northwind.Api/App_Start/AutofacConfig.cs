using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Northwind.Api.AutoMappings;
using Northwind.Repository.DbConnectionFactory;
using Northwind.Repository.UnitOfWork;
using Northwind.Service.Services;
using Northwind.Service.Services.Interfaces;
using System.Reflection;
using System.Web.Http;

namespace Northwind.Api.App_Start
{
    public class AutofacConfig
    {
        public static void Bootstrapper()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly‌​());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.Register(s => MappingConfiguration.CreateMapper(s.Resolve<IUnitOfWork>()))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //builder.RegisterAssemblyTypes(assemblies)
            //    .Where(t => t.Name.EndsWith("Service"))
            //    .AsImplementedInterfaces();

            builder.RegisterType<ProductsService>()
                .As<IProductsService>()
                .AsImplementedInterfaces();

            builder.RegisterType<DbConnectionFactory>()
                .As<IDbConnectionFactory>()
                .InstancePerLifetimeScope();


            IContainer container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}