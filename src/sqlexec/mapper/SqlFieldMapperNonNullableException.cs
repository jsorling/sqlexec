using System;

namespace Sorling.SqlExec.mapper;

public class SqlFieldMapperNonNullableException(string mapResult, string parameterName, string parmeterType) : Exception(
      $"Sql field is null and the parameter is non nullable, parameter name \"{parameterName}\" {parmeterType} in map result {mapResult}"
         )
{
}
