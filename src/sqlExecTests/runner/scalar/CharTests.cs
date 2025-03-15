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
      Assert.IsNotNull(o);
   }

   [TestMethod]
   public void CharNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char o = runner.ExecuteScalar<char, CharCommand>(new());
      Assert.AreEqual('1', o);
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
      Assert.IsNull(o);
   }

   [TestMethod]
   public void CharScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char? o = runner.ExecuteScalarAsync<char?, CharCommand>(new()).Result;
      Assert.IsNotNull(o);
   }

   [TestMethod]
   public void NullCharScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      char? o = runner.ExecuteScalarAsync<char?, NullCharCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
