namespace sqlExecTests.runner.scalar;

[TestClass]
public class DoubleTests
{
   public record DoubleCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(8.00000331 as real)";
   }

   [TestMethod]
   public void DoubleScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      double? o = runner.ExecuteScalar<double?, DoubleCommand>(new());
      Assert.IsTrue(o != null);
   }

   [TestMethod]
   public void DoubleNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      double o = runner.ExecuteScalar<double, DoubleCommand>(new());
      Assert.IsTrue((int)o == 8);
   }

   public record NullDoubleCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as real)";
   }

   [TestMethod]
   public void NullDoubleScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      double? o = runner.ExecuteScalar<double?, NullDoubleCommand>(new());
      Assert.IsTrue(o is null);
   }

   [TestMethod]
   public void DoubleScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      double? o = runner.ExecuteScalarAsync<double?, DoubleCommand>(new()).Result;
      Assert.IsTrue(o != null);
   }

   [TestMethod]
   public void NullDoubleScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      double? o = runner.ExecuteScalarAsync<double?, NullDoubleCommand>(new()).Result;
      Assert.IsTrue(o is null);
   }
}
