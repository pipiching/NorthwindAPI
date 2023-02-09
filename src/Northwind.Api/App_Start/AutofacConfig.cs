using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Northwind.Api.AutoMappings;
using Northwind.Repository.DbConnectionFactory;
using Northwind.Repository.UnitOfWork;
using System;
using System.Linq;
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

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.Register(s => MappingConfiguration.CreateMapper(s.Resolve<IUnitOfWork>()))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DbConnectionFactory>()
                .As<IDbConnectionFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            IContainer container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}