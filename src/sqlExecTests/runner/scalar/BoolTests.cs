namespace sqlExecTests.runner.scalar;

[TestClass]
public class BoolTests
{
   public record BoolCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(1 as bit)";
   }

   [TestMethod]
   public void BoolScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      bool? o = runner.ExecuteScalar<bool?, BoolCommand>(new());
      Assert.IsTrue(o);
   }

   [TestMethod]
   public void BoolNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      bool o = runner.ExecuteScalar<bool, BoolCommand>(new());
      Assert.IsTrue(o);
   }

   public record NullBoolCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as bit)";
   }

   [TestMethod]
   public void NullBoolScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      bool? o = runner.ExecuteScalar<bool?, NullBoolCommand>(new());
      Assert.IsNull(o);
   }

   [TestMethod]
   public void BoolScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      bool? o = runner.ExecuteScalarAsync<bool?, BoolCommand>(new()).Result;
      Assert.IsTrue(o);
   }

   [TestMethod]
   public void NullBoolScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      bool? o = runner.ExecuteScalarAsync<bool?, NullBoolCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
