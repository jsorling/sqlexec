using System;
using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.typemaps;
public class EnumMapper : TypeMapperBase
{
   private readonly Type _enumType;

   public override SqlDbType SqlDbType => SqlDbType.BigInt;

   public override bool IsNullable { get; init; }

   public EnumMapper(bool isNullAble, Type enumType) {
      IsNullable = isNullAble;
      _enumType = enumType.IsEnum ? enumType 
         : (Nullable.GetUnderlyingType(enumType)?.IsEnum ?? false) 
         ?  Nullable.GetUnderlyingType(enumType)! 
         : throw new ArgumentOutOfRangeException(nameof(enumType));
   }

   public override object? GetInstanceValue(object? instance)
     => instance is null or DBNull
      ? null
      : Enum.ToObject(_enumType, instance);

   public override object? GetSqlFieldValue(SqlDataReader sqlDataReader, int fieldIndex)
      => sqlDataReader.IsDBNull(fieldIndex)
      ? null
      : Enum.ToObject(_enumType, sqlDataReader.GetValue(fieldIndex));
}
