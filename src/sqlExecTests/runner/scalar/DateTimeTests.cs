using System;

namespace sqlExecTests.runner.scalar;

[TestClass]
public class DateTimeTests
{
   public record DateTimeCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast('2005-10-21' as date)";
   }

   [TestMethod]
   public void DateTimeScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      DateTime? o = runner.ExecuteScalar<DateTime?, DateTimeCommand>(new());
      Assert.IsTrue(o != null);
   }

   [TestMethod]
   public void DateTimeNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      DateTime o = runner.ExecuteScalar<DateTime, DateTimeCommand>(new());
      Assert.AreEqual(2005, o.Year);
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
      Assert.IsNull(o);
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
      Assert.IsNull(o);
   }
}
