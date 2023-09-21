using Sorling.SqlExec.mapper.attributes;
using Sorling.SqlExec.mapper.typemaps;
using System;
using System.Data;

namespace Sorling.SqlExec.mapper;

public class MapProperty<T>
{
   public Func<T, object> Getter { get; init; }

   public string PropertyName { get; init; }

   public string TypeName { get; init; }

   public Type PropertyType { get; init; }

   public SqlSourceAttribute? SqlSourceAttribute { get; init; }

   public string ParameterName => SqlSourceAttribute is not null ? SqlSourceAttribute.Name : PropertyName;

   public object Value(T instance)
      => Getter(instance ?? throw new ArgumentNullException(nameof(instance)));

   public SqlDbType SqlDbType => _typeMapper!.SqlDbType;

   private static ITypeMapper? _typeMapper;

   public MapProperty(Func<T, object> getter, string propertyName, string typeName, Type propertyType, SqlSourceAttribute? sqlParamAttribute) {
      Getter = getter ?? throw new ArgumentNullException(nameof(getter));
      PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
      TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
      PropertyType = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
      _typeMapper = MapType.GetTypeMapper(PropertyType);
      SqlSourceAttribute = sqlParamAttribute;
   }
}
