using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class TimeSpanMapper(bool isNullAble) : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.Time;

   public override bool IsNullable { get; init; } = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not TimeSpan ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetTimeSpan(fieldIndex);
}
