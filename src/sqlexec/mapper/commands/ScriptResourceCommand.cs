using System;
using System.Data;
using System.IO;

namespace Sorling.SqlExec.mapper.commands;

public abstract record ScriptResourceCommand : SqlExecBaseCommand
{
   public override CommandType SqlExecCommandType => CommandType.Text;

   public override string SqlExecSqlText => GetResource(SqlResourceText);

   protected abstract string SqlResourceText { get; }

   protected abstract Type AssemblyType { get; }

   private string GetResource(string name) {
      using Stream? resstream = AssemblyType.Assembly
         .GetManifestResourceStream(name ?? throw new ArgumentNullException(nameof(name)))
         ?? throw new FileNotFoundException($"Resource '{name}' not found in '{AssemblyType.Assembly.FullName}'");

      using MemoryStream ms = new();
      resstream.CopyTo(ms);
      ms.Position = 0;
      using StreamReader sr = new(ms);
      return sr.ReadToEnd();
   }
}

