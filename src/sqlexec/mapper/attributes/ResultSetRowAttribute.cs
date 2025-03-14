using System;

namespace Sorling.SqlExec.mapper.attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ResultSetRowAttribute(int index) : Attribute
{
   public int Index { get; init; } = index;
}
