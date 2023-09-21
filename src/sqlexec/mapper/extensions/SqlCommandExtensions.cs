using System.Data.SqlClient;

namespace Sorling.SqlExec.mapper.extensions;

public static class SqlCommandExtensions
{
   public static int? GetReturnValue(this SqlCommand sqlCommand)
      => sqlCommand.Parameters.Contains("@return_value")
      ? (int?)sqlCommand.Parameters["@return_value"].Value
      : default;
}
