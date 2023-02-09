using Dapper;
using Dapper.Contrib.Extensions;
using Northwind.Repository.DbConnectionFactory;
using Northwind.Repository.Repositories.Interfaces;
using Northwind.Repository.UnitOfWork;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using static Dapper.SqlBuilder;

namespace Northwind.Repository.Repositories
{
    public class RepositoryBase<TModel> : IRepositoryBase<TModel> where TModel : class, new()
    {
        private readonly IDbConnection _connection;

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, string connectionName)
        {
            _connection = dbConnectionFactory.GetConnection(connectionName);
        }

        protected IDbConnection Connection
        {
            get
            {
                if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        #region Delete

        public bool Delete(TModel obj, IUnitOfWorkBase uow = null)
        {
            return Connection.Delete(obj, uow?.Transaction);
        }

        public bool Delete(IEnumerable<TModel> list, IUnitOfWorkBase uow = null)
        {
            return Connection.Delete(list, uow?.Transaction);
        }

        public bool DeleteAll(IUnitOfWorkBase uow = null)
        {
            return Connection.DeleteAll<TModel>(uow?.Transaction);
        }

        #endregion Delete

        #region Get

        public TModel Get(dynamic id)
        {
            return SqlMapperExtensions.Get<TModel>(Connection, id);
        }

        public TModel GetNumber(string Number)
        {
            return SqlMapperExtensions.Get<TModel>(Connection, Number);
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            return Connection.GetAll<TModel>();
        }

        #endregion Get

        #region Insert

        public int Insert(TModel obj, IUnitOfWorkBase uow = null)
        {
            return (int)Connection.Insert(obj, uow?.Transaction);
        }

        public int Insert(IEnumerable<TModel> list, IUnitOfWorkBase uow = null)
        {
            return (int)Connection.Insert(list, uow?.Transaction);
        }

        #endregion Insert

        #region Update

        public bool Update(TModel obj, IUnitOfWorkBase uow = null)
        {
            return Connection.Update(obj, uow?.Transaction);
        }

        public bool Update(IEnumerable<TModel> list, IUnitOfWorkBase uow = null)
        {
            return Connection.Update(list, uow?.Transaction);
        }

        #endregion Update


        /// <summary>
        /// 取TEntity的TableName
        /// </summary>
        protected string GetTableNameMapper()
        {
            dynamic attributeTable = typeof(TModel).GetCustomAttributes(false)
                .FirstOrDefault(attr => attr.GetType().Name == "TableAttribute");

            return attributeTable != null ? attributeTable.Name : typeof(TModel).Name;
        }
        protected string GetWhereClause<TParameter>(TParameter parameters, string tableName = null)
        {
            if (tableName == null) tableName = GetTableNameMapper();
            //note the 'where' in-line comment is required, it is a replacement token
            string query = $"SELECT * FROM {tableName} /**where**/ ";

            SqlBuilder builder = new SqlBuilder();
            Template template = builder.AddTemplate(query);

            if (parameters == null)
            {
                return template.RawSql;
            }
            PropertyInfo[] properties = typeof(TParameter).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    string value = (string)property.GetValue(parameters, null);
                    if (!string.IsNullOrEmpty(value))
                    {
                        builder.Where($"{property.Name} = @{property.Name}");
                    }
                }
                else if (property.PropertyType == typeof(int))
                {
                    int value = (int)property.GetValue(parameters, null);
                    if (value != null)
                    {
                        builder.Where($"{property.Name} = @{property.Name}");
                    }
                }
                else
                {
                    if (property.GetValue(parameters, null) != null)
                    {
                        builder.Where($"{property.Name} = @{property.Name}");
                    }
                }
            }

            return template.RawSql;
        }
    }
}
