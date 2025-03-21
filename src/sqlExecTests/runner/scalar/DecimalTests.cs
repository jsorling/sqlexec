﻿namespace sqlExecTests.runner.scalar;

[TestClass]
public class DecimalTests
{
   public record DecimalCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(8.0012 as decimal)";
   }

   [TestMethod]
   public void DecimalScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      decimal? o = runner.ExecuteScalar<decimal?, DecimalCommand>(new());
      Assert.IsNotNull(o);
   }

   [TestMethod]
   public void DecimalNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      decimal o = runner.ExecuteScalar<decimal, DecimalCommand>(new());
      Assert.AreEqual(8, (int)o);
   }

   public record NullDecimalCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as decimal)";
   }

   [TestMethod]
   public void NullDecimalScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      decimal? o = runner.ExecuteScalar<decimal?, NullDecimalCommand>(new());
      Assert.IsNull(o);
   }

   [TestMethod]
   public void DecimalScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      decimal? o = runner.ExecuteScalarAsync<decimal?, DecimalCommand>(new()).Result;
      Assert.IsNotNull(o);
   }

   [TestMethod]
   public void NullDecimalScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      decimal? o = runner.ExecuteScalarAsync<decimal?, NullDecimalCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
