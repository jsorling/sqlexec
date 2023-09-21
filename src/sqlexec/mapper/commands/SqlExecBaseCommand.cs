using Sorling.SqlExec.mapper.attributes;
using System.Data;

namespace Sorling.SqlExec.mapper.commands;

public abstract record SqlExecBaseCommand
{
   [NotASqlParam]
   public abstract CommandType SqlExecCommandType { get; }

   [NotASqlParam]
   public abstract string SqlExecSqlText { get; }

   [NotASqlParam]
   public virtual bool SqlExecParametersLowerCase { get; } = true;

   [NotASqlParam]
   public int? SqlExecReturnValue { get; set; } = null;
}
