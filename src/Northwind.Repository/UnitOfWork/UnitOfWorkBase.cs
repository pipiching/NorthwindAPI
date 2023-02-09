using Northwind.Repository.DbConnectionFactory;
using System;
using System.Data;

namespace Northwind.Repository.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWorkBase
    {
        private IDbConnection _connection;
        private bool _disposed;

        public UnitOfWorkBase(IDbConnectionFactory dbConnectionFactory, string connectionName)
        {
            _connection = dbConnectionFactory.GetConnection(connectionName);
        }

        public IDbTransaction Transaction { get; private set; }

        protected abstract void ResetRepositories();

        public void BeginTransaction()
        {
            if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
            {
                _connection.Open();
            }
            Transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            Transaction.Commit();
            Transaction?.Dispose();
            Transaction = null;
            ResetRepositories();
        }

        public void RollBack()
        {
            Transaction?.Rollback();
            Transaction?.Dispose();
            Transaction = null;
            ResetRepositories();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Transaction?.Dispose();
                }
            }

            _disposed = true;
        }

        ~UnitOfWorkBase()
        {
            Dispose(false);
        }
    }
}
