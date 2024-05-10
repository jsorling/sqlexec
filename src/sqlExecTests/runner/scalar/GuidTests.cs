using System;

namespace sqlExecTests.runner.scalar;

[TestClass]
public class GuidTests
{
   public record GuidCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast('{3E824192-5C06-4D93-96BE-570E8A0F4029}' as uniqueidentifier)";
   }

   [TestMethod]
   public void GuidScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Guid? o = runner.ExecuteScalar<Guid?, GuidCommand>(new());
      Assert.IsTrue(o != null);
   }

   [TestMethod]
   public void GuidNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Guid o = runner.ExecuteScalar<Guid, GuidCommand>(new());
      Assert.IsTrue(o == Guid.Parse("{3E824192-5C06-4D93-96BE-570E8A0F4029}"));
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
