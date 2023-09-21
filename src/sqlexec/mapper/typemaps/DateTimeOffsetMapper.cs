using System;
using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class DateTimeOffsetMapper : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.DateTimeOffset;

   public override bool IsNullable { get; init; }

   public DateTimeOffsetMapper(bool isNullAble) => IsNullable = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not DateTimeOffset ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetDateTimeOffset(fieldIndex);
}
