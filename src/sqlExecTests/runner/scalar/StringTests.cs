namespace sqlExecTests.runner.scalar;

[TestClass]
public class StringTests
{
   public record StringCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select '123'";
   }

   [TestMethod]
   public void StringScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      string? o = runner.ExecuteScalar<string, StringCommand>(new());
      Assert.IsTrue(o == "123");
   }

   [TestMethod]
   public void StringNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      string o = runner.ExecuteScalar<string, StringCommand>(new());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
      Assert.IsTrue(o == "123");
   }

   public record NullStringCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as nvarchar(65))";
   }

   [TestMethod]
   public void NullStringScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      string? o = runner.ExecuteScalar<string, NullStringCommand>(new());
      Assert.IsTrue(o is null);
   }

   [TestMethod]
   public void StringScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      string? o = runner.ExecuteScalarAsync<string, StringCommand>(new()).Result;
      Assert.IsTrue(o != null);
   }

   [TestMethod]
   public void NullStringScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      string? o = runner.ExecuteScalarAsync<string, NullStringCommand>(new()).Result;
      Assert.IsTrue(o is null);
   }
}
