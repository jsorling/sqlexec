namespace sqlExecTests.runner.scalar;

[TestClass]
public class ShortTests
{
   public record ShortCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast( 3 as smallint)";
   }

   [TestMethod]
   public void ShortScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      short? o = runner.ExecuteScalar<short?, ShortCommand>(new());
      Assert.AreEqual(3, o);
   }

   [TestMethod]
   public void ShortNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      short o = runner.ExecuteScalar<short, ShortCommand>(new());
      Assert.AreEqual(3, o);
   }

   public record NullShortCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as smallint)";
   }

   [TestMethod]
   public void NullShortScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      short? o = runner.ExecuteScalar<short?, NullShortCommand>(new());
      Assert.IsNull(o);
   }

   [TestMethod]
   public void ShortScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      short? o = runner.ExecuteScalarAsync<short?, ShortCommand>(new()).Result;
      Assert.AreEqual(3, o);
   }

   [TestMethod]
   public void NullShortScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      short? o = runner.ExecuteScalarAsync<short?, NullShortCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
