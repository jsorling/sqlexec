using System;
using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;

public class GuidMapper : TypeMapperBase
{
   public override SqlDbType SqlDbType => SqlDbType.UniqueIdentifier;

   public override bool IsNullable { get; init; }

   public GuidMapper(bool isNullAble) => IsNullable = isNullAble;

   public override object? GetInstanceValue(object? instance)
     => instance is not Guid ? null : instance;

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex) ? null : sqlDataReader.GetGuid(fieldIndex);
}
