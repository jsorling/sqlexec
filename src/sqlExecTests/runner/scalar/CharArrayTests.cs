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
      Assert.IsNotNull(o);
   }

   [TestMethod]
   public void CharArrayNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      char[] o = runner.ExecuteScalar<char[], CharArrayCommand>(new());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
      Assert.IsNotNull(o);
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
      Assert.IsNull(o);
   }

   [TestMethod]
   public void CharArrayScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char[]? o = runner.ExecuteScalarAsync<char[], CharArrayCommand>(new()).Result;
      Assert.IsNotNull(o);
   }

   [TestMethod]
   public void NullCharArrayScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char[]? o = runner.ExecuteScalarAsync<char[]?, NullCharArrayCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
