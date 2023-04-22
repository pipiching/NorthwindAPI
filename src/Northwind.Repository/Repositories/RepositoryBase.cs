using Dapper;
using Dapper.Contrib.Extensions;
using Northwind.Common.Constants;
using Northwind.Repository.Attributes;
using Northwind.Repository.DbConnectionFactory;
using Northwind.Repository.Repositories.Interfaces;
using Northwind.Repository.UnitOfWork;
using System;
using System.Collections;
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

        /// <summary>
        /// Dynamic Query Table Where 
        /// <para>Define Attribute on Tsearch's Properties</para>
        /// <para>WhereColumnNameAttribute: columnName, Default is Property.Name</para>
        /// <para>WhereOperatorAttribute: operator, Default is equalto</para>
        /// </summary>
        /// <typeparam name="TSearch"></typeparam>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IEnumerable<TModel> Search<TSearch>(TSearch searchModel)
        {
            string sql =
            $@"
                SELECT *
                FROM {GetTableNameMapper()}
                /**where**/
                /**orderby**/
            ";

            SqlBuilder builder = new SqlBuilder();
            Template template = builder.AddTemplate(sql);

            GenerateWhereClause(builder, searchModel);

            builder.OrderBy("(SELECT NULL)");

            return Connection.Query<TModel>(template.RawSql, template.Parameters);
        }

        /// <summary>
        /// Create SQL Where clause
        /// <para>Get WhereOperatorAttribute from searchModel's property</para>
        /// <para>Determine the operator (=, &lt;, &gt;, LIKE...)</para>
        /// </summary>
        /// <typeparam name="TSearch"></typeparam>
        /// <param name="builder"></param>
        /// <param name="searchModel"></param>
        private void GenerateWhereClause<TSearch>(SqlBuilder builder, TSearch searchModel)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            PropertyInfo[] properties = typeof(TSearch).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(searchModel, null);
                if (value == null) continue;

                Type type = property.PropertyType;
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    type = Nullable.GetUnderlyingType(type);
                }
                TypeCode typeCode = Type.GetTypeCode(type);
                switch (typeCode)
                {
                    case TypeCode.String:
                        if (string.IsNullOrEmpty(value as string)) continue;
                        break;
                    case TypeCode.Object:
                        if (type.IsGenericType && (value as ICollection).Count == 0) continue;
                        if (type.IsArray && (value as Array).Length == 0) continue;
                        break;
                    default:
                        break;
                }

                object[] operatorAttrs = property.GetCustomAttributes(typeof(WhereOperatorAttribute), false);
                string whereOperator = (operatorAttrs.Length > 0) ? ((WhereOperatorAttribute)operatorAttrs[0]).Operator : TSqlOperator.EqualTo;
                object[] columnNameAttrs = property.GetCustomAttributes(typeof(WhereColumnNameAttribute), false);
                string columnName = (columnNameAttrs.Length > 0) ? ((WhereColumnNameAttribute)columnNameAttrs[0]).ColumnName : property.Name;

                dynamicParameters.Add(
                    property.Name,
                    whereOperator == TSqlOperator.Like ? $"%{value}%" : value
                );

                builder.Where($"{columnName} {whereOperator} @{property.Name}");
            }
            builder.AddParameters(dynamicParameters);
        }
    }
}
