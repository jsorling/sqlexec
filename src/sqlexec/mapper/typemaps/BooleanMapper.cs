using System.Data;
using Microsoft.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class BooleanMapper(bool isNullAble) : TypeMapperBase
{
   public override bool IsNullable { get; init; } = isNullAble;

   public override SqlDbType SqlDbType => SqlDbType.Bit;

   public override object? GetInstanceValue(object? instance)
      => instance is not bool ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetBoolean(fieldIndex);
}
