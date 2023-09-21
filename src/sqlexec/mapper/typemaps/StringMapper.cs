using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class StringMapper : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.NVarChar;

   public override bool IsNullable { get; init; }

   public StringMapper(bool isNullAble) => IsNullable = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not string ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetString(fieldIndex);
}
