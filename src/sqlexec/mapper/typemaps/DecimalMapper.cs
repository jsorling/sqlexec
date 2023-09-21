using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class DecimalMapper : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.Decimal;

   public override bool IsNullable { get; init; }

   public DecimalMapper(bool isNullAble) => IsNullable = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not decimal ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetDecimal(fieldIndex);
}
