using System.Data;

namespace xSql.Interface
{
    public interface ISqlHelper
    {
        DataTable SelectQuery<T>(string query, T data);
        void AlterDataQuery<T>(string query, T data);
        object AlterDataQueryScalar<T>(string query, T data);
    }
}