using Sorling.SqlExec.mapper.attributes;
using Sorling.SqlExec.mapper.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Sorling.SqlExec.mapper;

public static class MapCommand<T> where T : SqlExecBaseCommand
{
   private static readonly Dictionary<int, MapProperty<T>> _parameters;

   static MapCommand() {
      int i = 0;
      _parameters = new Dictionary<int, MapProperty<T>>(typeof(T).GetProperties()
         .Where(w => !w.GetCustomAttributes<NotASqlParamAttribute>().Any())
         .Select(s => new KeyValuePair<int, MapProperty<T>>(i++,
         new MapProperty<T>(MakeGetterDelegate(s), s.Name, s.PropertyType.Name, s.PropertyType
            , s.GetCustomAttributes<SqlSourceAttribute>().FirstOrDefault())
         )));
   }

   public static int ParamCount() => _parameters.Count;

   private static Func<T, object> MakeGetterDelegate(PropertyInfo pi) {
      ParameterExpression param = Expression.Parameter(typeof(T),
         (pi ?? throw new ArgumentNullException(nameof(pi))).Name);
      return Expression.Lambda<Func<T, object>>(Expression.Convert(Expression.Property(param, pi),
         typeof(object)), param).Compile();
   }

   public static IEnumerable<MapProperty<T>> CommandProperties => _parameters.Values;
}
