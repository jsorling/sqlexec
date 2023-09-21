using Sorling.SqlExec.mapper.typemaps;
using System;
using System.Reflection;

namespace Sorling.SqlExec.mapper;

internal class MapSqlField
{
   private static readonly NullabilityInfoContext _nullabilityctx = new();

   public Type Type { get; init; }

   public string Name { get; init; }

   public ITypeMapper ValueGetter { get; init; }

   public bool IsNullable => ValueGetter.IsNullable;

   public MapSqlField(ParameterInfo parameterInfo) {
      if (parameterInfo is null)
         throw new ArgumentNullException(nameof(parameterInfo));

      NullabilityInfo nainfo = _nullabilityctx.Create(parameterInfo);
      bool isnullable = nainfo.WriteState == NullabilityState.Nullable
         || (nainfo.WriteState == NullabilityState.Unknown
         && Nullable.GetUnderlyingType(parameterInfo.ParameterType) != null);

      Type = parameterInfo.ParameterType ?? throw new NotSupportedException();
      Name = parameterInfo.Name ?? throw new ArgumentNullException(nameof(parameterInfo));
      ValueGetter = parameterInfo.ParameterType.IsEnum
         ? new EnumMapper(isnullable, parameterInfo.ParameterType)
         : MapType.GetTypeMapper(parameterInfo.ParameterType, isnullable)
            ?? throw new NotSupportedException();
   }
}
