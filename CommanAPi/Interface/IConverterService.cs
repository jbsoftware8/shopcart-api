using System.Data;

namespace CommanApi.Interface
{
    public interface IConverterService
    {
        DbType ConvertToDbType(Type type);
    }
}
