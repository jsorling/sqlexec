namespace sqlExecTests.runner.scalar;

[TestClass]
public class LongTests
{
   public record LongCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(123 as bigint)";
   }

   [TestMethod]
   public void LongScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      long? o = runner.ExecuteScalar<long?, LongCommand>(new());
      Assert.IsTrue(o == 123);
   }

   public record NullLongCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as bigint)";
   }

   [TestMethod]
   public void NullLongScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      long? o = runner.ExecuteScalar<long?, NullLongCommand>(new());
      Assert.IsTrue(o is null);
   }

   [TestMethod]
   public void LongScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      long? o = runner.ExecuteScalarAsync<long?, LongCommand>(new()).Result;
      Assert.IsTrue(o == 123);
   }

   [TestMethod]
   public void NullLongScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      long? o = runner.ExecuteScalarAsync<long?, NullLongCommand>(new()).Result;
      Assert.IsTrue(o is null);
   }
}
