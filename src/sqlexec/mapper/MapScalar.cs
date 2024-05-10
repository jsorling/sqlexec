using Sorling.SqlExec.mapper.typemaps;
using System;

namespace Sorling.SqlExec.mapper;

public class MapScalar<T>
{
   private static readonly ITypeMapper _typeMap;

   static MapScalar() {
      ITypeMapper? typemapper = MapType.GetTypeMapper(typeof(T)) ?? throw new NotSupportedException();
      _typeMap = typemapper;
   }

   public static T? MapScalarValue(object? instance)
      => (T?)_typeMap.GetInstanceValue(instance);
}
