using Sorling.SqlExec.mapper.results;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Sorling.SqlExec.mapper;

internal static class MapResult<T> where T : SqlExecBaseResult
{
   private static readonly ConstructorInfo _constructor;

   private static readonly Dictionary<int, MapSqlField> _mappings;

   private static readonly int _parametercount;

   static MapResult() {
      ConstructorInfo[]? ctor = typeof(T).GetConstructors();
      if (ctor.Length < 1)
         throw new ArgumentOutOfRangeException(typeof(T).Name,
            $"{typeof(T).Name} does not contain any public constructor");

      ParameterInfo[]? ctorparams = ctor[0].GetParameters();
      if (ctorparams.Length < 1)
         throw new ArgumentOutOfRangeException(typeof(T).Name,
            $"{typeof(T).Name}'s first constructor does not require any parameters");

      _constructor = ctor[0];

      int key = 0;
      _mappings = new(ctorparams.OrderBy(o => o.Position).Select(
         s => new KeyValuePair<int, MapSqlField>(key++, new(s))));

      _parametercount = _mappings.Count;
   }

   public static T MapRow(SqlDataReader sqlDataReader) {
      ArgumentNullException.ThrowIfNull(sqlDataReader);
      object?[] args = new object[_parametercount];

      for (int i = 0; i < _parametercount; i++) {
         object? res;
         try {
            res = _mappings[i]!.ValueGetter!.GetSqlFieldValue(sqlDataReader, i);
         }
         catch (InvalidCastException e) {
            string cname = sqlDataReader.GetName(i);
            if (string.IsNullOrWhiteSpace(cname))
               cname = "<No column name>";

            throw new InvalidCastException(
               $"Invalid cast field index {i}, column {cname}, field type {sqlDataReader.GetDataTypeName(i)}, mapping type {_mappings[i].Type.Name}, in result type {typeof(T).Name}"
               , e);
            ;
         }

         if (res is null && !_mappings[i]!.IsNullable)
            throw new SqlFieldMapperNonNullableException(typeof(T).Name, _mappings[i]!.Name,
               _mappings[i]!.Type.Name);
         args[i] = res;
      }

      return (T)_constructor.Invoke(args);
   }
}
