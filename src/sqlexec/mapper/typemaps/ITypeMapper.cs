using System.Data;
using Microsoft.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public interface ITypeMapper
{
   public bool IsNullable { get; }

   public object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex);

   public SqlDbType SqlDbType { get; }

   public object? GetInstanceValue(object? instance);
}
