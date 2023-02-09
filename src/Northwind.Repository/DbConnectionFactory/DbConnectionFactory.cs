using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Northwind.Repository.DbConnectionFactory
{
    public class DbConnectionFactory : IDbConnectionFactory, IDisposable
    {
        private readonly Dictionary<string, IDbConnection> _dbConnections = new Dictionary<string, IDbConnection>();

        public IDbConnection GetConnection(string connectionName)
        {
            try
            {
                if (_dbConnections.ContainsKey(connectionName) == false)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                    SqlConnection connection = new SqlConnection(connectionString);
                    _dbConnections.Add(connectionName, connection);
                }

                return _dbConnections[connectionName];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Clear()
        {
            foreach (var connection in _dbConnections)
            {
                connection.Value.Close();
            }
            _dbConnections.Clear();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}
