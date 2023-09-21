using System;

namespace Sorling.SqlExec.mapper;

public class SqlFieldMapperNonNullableException : Exception
{
   public SqlFieldMapperNonNullableException(string mapResult, string parameterName, string parmeterType)
      : base(
         $"Sql field is null and the parameter is non nullable, parameter name \"{parameterName}\" {parmeterType} in map result {mapResult}"
         ) { }
}
