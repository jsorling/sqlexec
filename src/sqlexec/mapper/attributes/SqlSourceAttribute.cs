using System;

namespace Sorling.SqlExec.mapper.attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
public class SqlSourceAttribute(string name, string? fullname = null, string? sourceType = null) : Attribute
{
   public string Name { get; init; } = name ?? "???";

   public string? FullName { get; init; } = fullname ?? "???";

   public string? SourceType { get; init; } = sourceType ?? "???";
}
