using System;
using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class DateTimeMapper : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.DateTime;

   public override bool IsNullable { get; init; }

   public DateTimeMapper(bool isNullAble) => IsNullable = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not DateTime ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetDateTime(fieldIndex);
}
