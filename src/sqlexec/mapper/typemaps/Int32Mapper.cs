using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class Int32Mapper : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.Int;

   public override bool IsNullable { get; init; }

   public Int32Mapper(bool isNullAble) => IsNullable = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not int ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetInt32(fieldIndex);
}
