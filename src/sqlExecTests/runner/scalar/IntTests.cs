namespace sqlExecTests.runner.scalar;

[TestClass]
public class IntTests
{
   public record IntCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(3 as int)";
   }

   [TestMethod]
   public void IntScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int? o = runner.ExecuteScalar<int?, IntCommand>(new());
      Assert.AreEqual(3, o);
   }

   [TestMethod]
   public void IntNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int o = runner.ExecuteScalar<int, IntCommand>(new());
      Assert.AreEqual(3, o);
   }

   public record NullIntCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as int)";
   }

   [TestMethod]
   public void NullIntScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int? o = runner.ExecuteScalar<int?, NullIntCommand>(new());
      Assert.IsNull(o);
   }

   [TestMethod]
   public void IntScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int? o = runner.ExecuteScalarAsync<int?, IntCommand>(new()).Result;
      Assert.AreEqual(3, o);
   }

   [TestMethod]
   public void NullIntScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int? o = runner.ExecuteScalarAsync<int?, NullIntCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
