using Microsoft.Extensions.Configuration;
using CommanApi.Interface;
using System.Data.Common;
using System.Data.SqlClient;

namespace CommanApi.Repository
{
    public class SqlConnectionProvider : ISqlConnectionProvider
    {
        private readonly SqlConnection _connection;
        public SqlConnectionProvider(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("sqlconnection"));
        }
        public void Dispose()
        {
            _connection.Dispose();
            GC.SuppressFinalize(this);
        }

        public SqlConnection GetSqlConnection()
        {
            return _connection;
        }
    }
}
