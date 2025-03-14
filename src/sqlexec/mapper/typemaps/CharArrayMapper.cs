using System.Data;
using Microsoft.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class CharArrayMapper(bool isNullAble) : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.Char;

   public override bool IsNullable { get; init; } = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is string str ? str.ToCharArray() : null;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetString(fieldIndex).ToCharArray();
}

