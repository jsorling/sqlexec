using System;

namespace sqlExecTests.runner;

[TestClass]
public class InvalidCastTests
{
   public record InvalidCastCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(1 as int) anint";

      public record InvalidRecordTest(string First) : SqlExecBaseResult;
   }

   [TestMethod]
   public void InvalidQueryResult() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      InvalidCastException e = Assert.ThrowsException<InvalidCastException>(
         () => runner.Query<InvalidCastCommand.InvalidRecordTest, InvalidCastCommand>(
            new InvalidCastCommand()));
      Console.WriteLine(e.Message);
   }

   [TestMethod]
   public void InvalidScalarCast() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      string? result = runner.ExecuteScalar<string, InvalidCastCommand>(new InvalidCastCommand());
      Console.WriteLine(result);
   }
}
