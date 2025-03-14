using Sorling.SqlExec.mapper.results;
using System;
using Microsoft.Data.SqlClient;

namespace Sorling.SqlExec.mapper.extensions;

public static class SqlDataReaderExtensions
{
   public static T SqlExecMapRow<T>(this SqlDataReader sqlDataReader) where T : SqlExecBaseResult
      => MapResult<T>.MapRow(sqlDataReader ?? throw new ArgumentNullException(nameof(sqlDataReader)));
}
