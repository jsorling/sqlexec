using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class FloatMapper : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.Float;

   public override bool IsNullable { get; init; }

   public FloatMapper(bool isNullAble) => IsNullable = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is float fl
     ? fl
     : instance is double db
     ? (float)db
     : null;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetFloat(fieldIndex);
}
