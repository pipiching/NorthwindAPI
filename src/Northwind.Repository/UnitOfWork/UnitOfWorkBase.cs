using Northwind.Repository.DbConnectionFactory;
using Northwind.Repository.Repositories;
using Northwind.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace Northwind.Repository.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWorkBase
    {
        private IDbConnection _connection;
        private string _connectionName;
        private IDbConnectionFactory _dbConnectionFactory;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        public UnitOfWorkBase(IDbConnectionFactory dbConnectionFactory, string connectionName)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionName = connectionName;
            _connection = dbConnectionFactory.GetConnection(connectionName);
        }

        public IDbTransaction Transaction { get; private set; }

        protected virtual void ResetRepositories()
        {
            _repositories = null;
        }

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

        public IRepositoryBase<T> GetRepository<T>() where T : class, new()
        {
            if (_repositories == null)
            {
                //_repositories = new Hashtable();
                _repositories = new Dictionary<string, object>();
            }

            string type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                Type repositoryType = typeof(RepositoryBase<>);

                object repositoryInstance =
                    Activator.CreateInstance(
                        repositoryType.MakeGenericType(typeof(T)), _dbConnectionFactory, _connectionName);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepositoryBase<T>)_repositories[type];
        }

        ~UnitOfWorkBase()
        {
            Dispose(false);
        }
    }
}
