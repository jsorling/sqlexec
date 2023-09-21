namespace sqlExecTests.runner.scalar;

[TestClass]
public class CharTests
{
   public record CharCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast('1' as char)";
   }

   [TestMethod]
   public void CharScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char? o = runner.ExecuteScalar<char?, CharCommand>(new());
      Assert.IsTrue(o != null);
   }

   public record NullCharCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as char)";
   }

   [TestMethod]
   public void NullCharScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char? o = runner.ExecuteScalar<char?, NullCharCommand>(new());
      Assert.IsTrue(o is null);
   }

   [TestMethod]
   public void CharScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char? o = runner.ExecuteScalarAsync<char?, CharCommand>(new()).Result;
      Assert.IsTrue(o != null);
   }

   [TestMethod]
   public void NullCharScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char? o = runner.ExecuteScalarAsync<char?, NullCharCommand>(new()).Result;
      Assert.IsTrue(o is null);
   }
}
