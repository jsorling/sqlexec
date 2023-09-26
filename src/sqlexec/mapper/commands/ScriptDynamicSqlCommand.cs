using Sorling.SqlExec.mapper.attributes;
using System.Data;

namespace Sorling.SqlExec.mapper.commands;

public record ScriptDynamicSqlCommand([NotASqlParam] string Sql) : SqlExecBaseCommand
{
   public override CommandType SqlExecCommandType => CommandType.Text;

   public override string SqlExecSqlText => Sql;
}
