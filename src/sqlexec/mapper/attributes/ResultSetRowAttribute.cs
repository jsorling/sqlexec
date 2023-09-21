using System;

namespace Sorling.SqlExec.mapper.attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ResultSetRowAttribute : Attribute
{
   public int Index { get; init; }

   public ResultSetRowAttribute(int index) => Index = index;
}
