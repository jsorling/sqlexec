using System.Data;
using Microsoft.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class ByteGetterMapper(bool isNullAble) : TypeMapperBase
{
   public override bool IsNullable { get; init; } = isNullAble;

   public override SqlDbType SqlDbType => SqlDbType.TinyInt;

   public override object? GetInstanceValue(object? instance)
     => instance is not byte ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetByte(fieldIndex);
}
