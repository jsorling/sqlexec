namespace Sorling.SqlExec.mapper.results;

public record SqlExecBaseResult()
{
   public int? SqlExecReturnValue { get; set; } = default;
}
