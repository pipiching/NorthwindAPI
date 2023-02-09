using System.Data;

namespace Northwind.Repository.DbConnectionFactory
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection(string connectionName);

        void Clear();
    }
}
