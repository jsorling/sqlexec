using System;

namespace sqlExecTests.runner.scalar;

[TestClass]
public class DateTimeTests
{
   public record DateTimeCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select getdate()";
   }

   [TestMethod]
   public void DateTimeScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      DateTime? o = runner.ExecuteScalar<DateTime?, DateTimeCommand>(new());
      Assert.IsTrue(o != null);
   }

   public record NullDateTimeCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as datetime)";
   }

   [TestMethod]
   public void NullDateTimeScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      DateTime? o = runner.ExecuteScalar<DateTime?, NullDateTimeCommand>(new());
      Assert.IsTrue(o is null);
   }

   [TestMethod]
   public void DateTimeScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      DateTime? o = runner.ExecuteScalarAsync<DateTime?, DateTimeCommand>(new()).Result;
      Assert.IsTrue(o != null);
   }

   [TestMethod]
   public void NullDateTimeScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      DateTime? o = runner.ExecuteScalarAsync<DateTime?, NullDateTimeCommand>(new()).Result;
      Assert.IsTrue(o is null);
   }
}
