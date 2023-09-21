using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class Int16Mapper : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.SmallInt;

   public override bool IsNullable { get; init; }

   public Int16Mapper(bool isNullAble) => IsNullable = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not short ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetInt16(fieldIndex);
}
