using System.Data;
using Microsoft.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public abstract class TypeMapperBase : ITypeMapper
{
   public abstract bool IsNullable { get; init; }

   public abstract SqlDbType SqlDbType { get; }

   public abstract object? GetInstanceValue(object? instance);

   public abstract object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex);
}

