using Sorling.SqlExec.mapper.attributes;
using System.Data;
using System.IO;

namespace Sorling.SqlExec.mapper.commands;

public abstract record ScriptFileCommand : SqlExecBaseCommand
{
   public override CommandType SqlExecCommandType => CommandType.Text;

   public override string SqlExecSqlText => File.ReadAllText(SqlExecFilePath);

   [NotASqlParam]
   public abstract string SqlExecFilePath { get; }
}
