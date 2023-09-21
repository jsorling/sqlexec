using System;

namespace sqlExecTests.runner.scalar;

[TestClass]
public class GuidTests
{
   public record GuidCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select newid()";
   }

   [TestMethod]
   public void GuidScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Guid? o = runner.ExecuteScalar<Guid?, GuidCommand>(new());
      Assert.IsTrue(o != null);
   }

   public record NullGuidCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as uniqueidentifier)";
   }

   [TestMethod]
   public void NullGuidScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Guid? o = runner.ExecuteScalar<Guid?, NullGuidCommand>(new());
      Assert.IsTrue(o is null);
   }

   [TestMethod]
   public void GuidScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Guid? o = runner.ExecuteScalarAsync<Guid?, GuidCommand>(new()).Result;
      Assert.IsTrue(o != null);
   }

   [TestMethod]
   public void NullGuidScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Guid? o = runner.ExecuteScalarAsync<Guid?, NullGuidCommand>(new()).Result;
      Assert.IsTrue(o is null);
   }
}
