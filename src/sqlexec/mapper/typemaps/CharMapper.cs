using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class CharMapper : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.Char;

   public override bool IsNullable { get; init; }

   public CharMapper(bool isNullAble) => IsNullable = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is string str ? str.ToCharArray()[0] : null;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
     => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetChar(fieldIndex);
}
