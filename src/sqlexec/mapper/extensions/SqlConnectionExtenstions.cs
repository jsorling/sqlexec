using Sorling.SqlExec.mapper.commands;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.extensions;

public static class SqlConnectionExtenstions
{
   public static SqlCommand SqlExecCreatePreparedCommand<P>(this SqlConnection sqlConnection, P parameters)
      where P : SqlExecBaseCommand {
      if (parameters is null)
         throw new ArgumentNullException(nameof(parameters));

      SqlCommand sqlcommand = (sqlConnection
         ?? throw new ArgumentNullException(nameof(sqlConnection))).CreateCommand();
      sqlcommand.CommandText = parameters.SqlExecSqlText;
      sqlcommand.CommandType = parameters.SqlExecCommandType;

      foreach (MapProperty<P> cmp in MapCommand<P>.CommandProperties) {
         string parametername =
            parameters.SqlExecParametersLowerCase && cmp.SqlSourceAttribute is null
            ? cmp.ParameterName.ToLower()
            : cmp.SqlSourceAttribute?.Name is not null
            ? cmp.SqlSourceAttribute.Name
            : cmp.ParameterName;

         if (!parametername.StartsWith('@')) {
            parametername = '@' + parametername;
         }

         object value = cmp.Value(parameters);

         _ = value is not null
            ? sqlcommand.Parameters.AddWithValue(parametername, value)
            : sqlcommand.Parameters.Add(parametername, cmp.SqlDbType).Value = DBNull.Value;
      }

      if (parameters.SqlExecCommandType == CommandType.StoredProcedure)
         sqlcommand.Parameters.Add("@return_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

      return sqlcommand;
   }
}
