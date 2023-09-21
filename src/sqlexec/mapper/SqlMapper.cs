using Sorling.SqlExec.mapper.results;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Sorling.SqlExec.mapper;

public static class SqlMapper
{
   public static async Task<IEnumerable<T>> ReadRecordSetAsync<T>(this SqlDataReader sqlDataReader,
      CancellationToken token = default) where T : SqlExecBaseResult {
      List<T> tor = new();

      while (await sqlDataReader.ReadAsync(token).ConfigureAwait(false)) {
         tor.Add(MapResult<T>.MapRow(sqlDataReader));
      }

      return tor;
   }
}
