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
      Assert.AreEqual(123, o);
   }

   [TestMethod]
   public void LongNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      long o = runner.ExecuteScalar<long, LongCommand>(new());
      Assert.AreEqual(123, o);
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
      Assert.IsNull(o);
   }

   [TestMethod]
   public void LongScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      long? o = runner.ExecuteScalarAsync<long?, LongCommand>(new()).Result;
      Assert.AreEqual(123, o);
   }

   [TestMethod]
   public void NullLongScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      long? o = runner.ExecuteScalarAsync<long?, NullLongCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
