using System;

namespace Sorling.SqlExec.mapper.attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
public class NotASqlParamAttribute : Attribute
{
}
