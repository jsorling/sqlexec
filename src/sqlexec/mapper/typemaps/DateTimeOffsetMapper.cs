using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class DateTimeOffsetMapper(bool isNullAble) : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.DateTimeOffset;

   public override bool IsNullable { get; init; } = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not DateTimeOffset ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetDateTimeOffset(fieldIndex);
}
