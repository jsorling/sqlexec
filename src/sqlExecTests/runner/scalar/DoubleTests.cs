﻿namespace sqlExecTests.runner.scalar;

[TestClass]
public class DoubleTests
{
   public record DoubleCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(8.00000331 as real)";
   }

   [TestMethod]
   public void DoubleScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      double? o = runner.ExecuteScalar<double?, DoubleCommand>(new());
      Assert.IsNotNull(o);
   }

   [TestMethod]
   public void DoubleNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      double o = runner.ExecuteScalar<double, DoubleCommand>(new());
      Assert.AreEqual(8, (int)o);
   }

   public record NullDoubleCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as real)";
   }

   [TestMethod]
   public void NullDoubleScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      double? o = runner.ExecuteScalar<double?, NullDoubleCommand>(new());
      Assert.IsNull(o);
   }

   [TestMethod]
   public void DoubleScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      double? o = runner.ExecuteScalarAsync<double?, DoubleCommand>(new()).Result;
      Assert.IsNotNull(o);
   }

   [TestMethod]
   public void NullDoubleScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      double? o = runner.ExecuteScalarAsync<double?, NullDoubleCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
