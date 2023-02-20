using Northwind.Repository.DbConnectionFactory;

namespace Northwind.Repository.UnitOfWork
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        private const string connectionName = "Northwind";
        private IDbConnectionFactory _dbConnectionFactory;

        public UnitOfWork(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory, connectionName)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        protected override void ResetRepositories()
        {
            base.ResetRepositories();
        }
    }
}
