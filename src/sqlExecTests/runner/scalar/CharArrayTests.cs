namespace sqlExecTests.runner.scalar;

[TestClass]
public class CharArrayTests
{
   public record CharArrayCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast('123' as char)";
   }

   [TestMethod]
   public void CharArrayScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char[]? o = runner.ExecuteScalar<char[]?, CharArrayCommand>(new());
      Assert.IsTrue(o != null);
   }

   public record NullCharArrayCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as char)";
   }

   [TestMethod]
   public void NullCharArrayScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char[]? o = runner.ExecuteScalar<char[]?, NullCharArrayCommand>(new());
      Assert.IsTrue(o is null);
   }

   [TestMethod]
   public void CharArrayScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char[]? o = runner.ExecuteScalarAsync<char[], CharArrayCommand>(new()).Result;
      Assert.IsTrue(o != null);
   }

   [TestMethod]
   public void NullCharArrayScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char[]? o = runner.ExecuteScalarAsync<char[]?, NullCharArrayCommand>(new()).Result;
      Assert.IsTrue(o is null);
   }
}
