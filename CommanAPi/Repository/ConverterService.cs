using CommanApi.Interface;
using System.Data;

namespace CommanApi.Repository
{
    public class ConverterService : IConverterService
    {
        public DbType ConvertToDbType(Type type)
        {
            if (type == typeof(int) || type == typeof(int?) || type == typeof(short) || type == typeof(short?))
            {
                return DbType.Int32;
            }

            if (type == typeof(float) || type == typeof(float?))
            {
                return DbType.Single;
            }

            if (type == typeof(double) || type == typeof(double?))
            {
                return DbType.Double;
            }

            if (type == typeof(bool) || type == typeof(bool?))
            {
                return DbType.Boolean;
            }

            if (type == typeof(string))
            {
                return DbType.String;
            }

            if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return DbType.DateTime;
            }

            if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
            {
                return DbType.DateTimeOffset;
            }

            return type == typeof(char) ? DbType.StringFixedLength : DbType.String;
        }
    }
}
