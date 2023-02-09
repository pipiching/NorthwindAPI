using Dapper;
using Northwind.Repository.Entities;
using Northwind.Repository.Models;
using Northwind.Repository.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace Northwind.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> Search(ProductSearchModel searchModel)
        {
            string sql =
            $@"
                SELECT *
                FROM Products
                WHERE 1 = 1
            ";

            DynamicParameters dynamicParameters = new DynamicParameters();

            if (searchModel.ProductID != null)
            {
                sql += " AND ProductID=@ProductID";
                dynamicParameters.Add("ProductID", searchModel.ProductID);
            }
            if (!string.IsNullOrEmpty(searchModel.ProductName))
            {
                sql += " AND ProductName=@ProductName";
                dynamicParameters.Add("ProductName", searchModel.ProductName);
            }

            return _connection.Query<Product>(sql, dynamicParameters);
        }
    }
}
