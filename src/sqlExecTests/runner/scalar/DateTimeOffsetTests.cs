﻿using System;

namespace sqlExecTests.runner.scalar;

[TestClass]
public class DateTimeOffsetTests
{
   public record DateTimeOffsetCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast('12-10-25 12:32:10 +01:00' as datetimeoffset)";
   }

   [TestMethod]
   public void DateTimeOffsetScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      DateTimeOffset o = runner.ExecuteScalar<DateTimeOffset, DateTimeOffsetCommand>(new());
      Assert.AreEqual(344, o.DayOfYear);
   }

   [TestMethod]
   public void DateTimeOffsetNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      DateTimeOffset o = runner.ExecuteScalar<DateTimeOffset, DateTimeOffsetCommand>(new());
      Assert.AreEqual(12, o.Hour);
   }

   public record NullDateTimeOffsetCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as datetimeoffset)";
   }

   [TestMethod]
   public void NullDateTimeOffsetScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      DateTimeOffset? o = runner.ExecuteScalar<DateTimeOffset?, NullDateTimeOffsetCommand>(new());
      Assert.IsNull(o);
   }

   [TestMethod]
   public void DateTimeOffsetScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      DateTimeOffset? o = runner.ExecuteScalarAsync<DateTimeOffset?, DateTimeOffsetCommand>(new()).Result;
      Assert.IsTrue(o != null);
   }

   [TestMethod]
   public void NullDateTimeOffsetScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      DateTimeOffset? o = runner.ExecuteScalarAsync<DateTimeOffset?, NullDateTimeOffsetCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
