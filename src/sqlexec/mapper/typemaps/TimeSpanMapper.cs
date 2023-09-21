using System;
using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class TimeSpanMapper : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.Time;

   public override bool IsNullable { get; init; }

   public TimeSpanMapper(bool isNullAble) => IsNullable = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not TimeSpan ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetTimeSpan(fieldIndex);
}
