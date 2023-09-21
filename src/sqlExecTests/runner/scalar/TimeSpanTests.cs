using System;

namespace sqlExecTests.runner.scalar;

[TestClass]
public class TimeSpanTests
{
   public record TimeSpanCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast('12:34:54.1237' as time)";
   }

   [TestMethod]
   public void TimeSpanScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      TimeSpan? o = runner.ExecuteScalar<TimeSpan?, TimeSpanCommand>(new());
      Assert.IsTrue(o != null);
   }

   public record NullTimeSpanCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as time)";
   }

   [TestMethod]
   public void NullTimeSpanScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      TimeSpan? o = runner.ExecuteScalar<TimeSpan?, NullTimeSpanCommand>(new());
      Assert.IsTrue(o is null);
   }

   [TestMethod]
   public void TimeSpanScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      TimeSpan? o = runner.ExecuteScalarAsync<TimeSpan?, TimeSpanCommand>(new()).Result;
      Assert.IsTrue(o != null);
   }

   [TestMethod]
   public void NullTimeSpanScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      TimeSpan? o = runner.ExecuteScalarAsync<TimeSpan?, NullTimeSpanCommand>(new()).Result;
      Assert.IsTrue(o is null);
   }
}
