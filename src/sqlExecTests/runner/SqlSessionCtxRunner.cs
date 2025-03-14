using System;
using Microsoft.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace sqlExecTests.runner;

internal class SqlSessionCtxRunner(string sqlConnectionString, Dictionary<string, string> keys) : SqlExecRunner(sqlConnectionString)
{
   protected Dictionary<string, string> Keys { get; init; } = keys ?? throw new ArgumentNullException(nameof(keys));

   protected override async Task OpenConnectionAsync(SqlConnection sqlConnection, CancellationToken cancellationToken) {
      await base.OpenConnectionAsync(sqlConnection, cancellationToken);
      if (Keys.Count != 0) {
         SqlCommand sc = sqlConnection.CreateCommand();
         string cmdtxt = string.Empty;
         foreach (KeyValuePair<string, string> key in Keys) {
            cmdtxt = $"exec sp_set_session_context '{key.Key}', {key.Value};";
         }

         sc.CommandType = CommandType.Text;
         sc.CommandText = cmdtxt;
         _ = await sc.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
      }
   }

   protected override void OpenConnection(SqlConnection sqlConnection) {
      base.OpenConnection(sqlConnection);
      SqlCommand sc = sqlConnection.CreateCommand();
      string cmdtxt = string.Empty;
      foreach (KeyValuePair<string, string> key in Keys) {
         cmdtxt = $"exec sp_set_session_context '{key.Key}', {key.Value};";
      }

      sc.CommandType = CommandType.Text;
      sc.CommandText = cmdtxt;
      _ = sc.ExecuteNonQuery();
   }
}
