using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class Int64Mapper : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.BigInt;

   public override bool IsNullable { get; init; }

   public Int64Mapper(bool isNullAble) => IsNullable = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not long ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetInt64(fieldIndex);
}
