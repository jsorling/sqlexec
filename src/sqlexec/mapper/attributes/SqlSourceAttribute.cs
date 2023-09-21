using System;

namespace Sorling.SqlExec.mapper.attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
public class SqlSourceAttribute : Attribute
{
   public string Name { get; init; }

   public string? FullName { get; init; }

   public string? SourceType { get; init; }

   public SqlSourceAttribute(string name, string? fullname = null, string? sourceType = null) {
      Name = name ?? "???";
      FullName = fullname ?? "???";
      SourceType = sourceType ?? "???";
   }
}
