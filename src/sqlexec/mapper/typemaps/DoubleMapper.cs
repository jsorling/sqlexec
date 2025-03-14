using System.Data;
using Microsoft.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class DoubleMapper(bool isNullAble) : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.Float;

   public override bool IsNullable { get; init; } = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is float single
     ? (double)single
     : instance is double
     ? instance
     : null;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetDouble(fieldIndex);
}
