using Northwind.Repository.DbConnectionFactory;
using Northwind.Repository.Entities;
using Northwind.Repository.Repositories.Interfaces;

namespace Northwind.Repository.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbConnectionFactory dbConnectionFactory, string connectionName)
            : base(dbConnectionFactory, connectionName)
        {

        }

        //public IEnumerable<Product> Search(ProductSearchModel searchModel)
        //{
        //    string sql =
        //    $@"
        //        SELECT *
        //        FROM {GetTableNameMapper()}
        //        /**where**/
        //        /**orderby**/
        //    ";

        //    SqlBuilder builder = new SqlBuilder();
        //    Template template = builder.AddTemplate(sql);

        //    if (searchModel.ProductID != null)
        //    {
        //        builder.Where($"ProductID = @ProductID", new { searchModel.ProductID });
        //    }
        //    if (!string.IsNullOrEmpty(searchModel.ProductName))
        //    {
        //        builder.Where($"ProductName = @ProductName", new { searchModel.ProductName });
        //    }

        //    builder.OrderBy("(SELECT NULL)");

        //    return Connection.Query<Product>(template.RawSql, template.Parameters);
        //}
    }
}
