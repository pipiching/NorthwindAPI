using Northwind.Repository.DbConnectionFactory;
using Northwind.Repository.Repositories;
using Northwind.Repository.Repositories.Interfaces;

namespace Northwind.Repository.UnitOfWork
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        private const string connectionName = "Northwind";
        private IDbConnectionFactory _dbConnectionFactory;
        private IProductRepository _productRepository;

        public UnitOfWork(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory, connectionName)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IProductRepository ProductRepository =>
            _productRepository ?? (_productRepository = new ProductRepository(_dbConnectionFactory, connectionName));


        protected override void ResetRepositories()
        {
            _productRepository = null;
        }
    }
}
