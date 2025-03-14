using System.Data;
using Microsoft.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class ByteArrayMapper(bool isNullAble) : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.Binary;

   public override bool IsNullable { get; init; } = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not byte[]? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex) {
      if (sqlDataReader.IsDBNull(fieldIndex))
         return null;
      long size = sqlDataReader.GetBytes(fieldIndex, 0, null, 0, 1);
      byte[]? tor = new byte[size];
      _ = sqlDataReader.GetBytes(fieldIndex, 0, tor, 0, (int)size);
      return tor;
   }
}
