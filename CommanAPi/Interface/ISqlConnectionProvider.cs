using System.Data.SqlClient;

namespace CommanApi.Interface
{
    public interface ISqlConnectionProvider : IDisposable
    {
        SqlConnection GetSqlConnection();
    }
}
