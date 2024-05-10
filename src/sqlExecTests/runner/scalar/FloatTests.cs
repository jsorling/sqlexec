namespace sqlExecTests.runner.scalar;
[TestClass]
public class FloatTests
{
   public record FloatCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(8.0003 as float)";
   }

   [TestMethod]
   public void FloatScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      float? o = runner.ExecuteScalar<float?, FloatCommand>(new());
      Assert.IsTrue((int)o!.Value == 8);
   }

   [TestMethod]
   public void FloatNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      float o = runner.ExecuteScalar<float, FloatCommand>(new());
      Assert.IsTrue((int)o == 8);
   }

   public record NullFloatCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as float)";
   }

   [TestMethod]
   public void NullFloatScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      float? o = runner.ExecuteScalar<float?, NullFloatCommand>(new());
      Assert.IsTrue(o is null);
   }

   [TestMethod]
   public void FloatScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      float? o = runner.ExecuteScalarAsync<float?, FloatCommand>(new()).Result;
      Assert.IsTrue((int)o!.Value == 8);
   }

   [TestMethod]
   public void NullFloatScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      float? o = runner.ExecuteScalarAsync<float?, NullFloatCommand>(new()).Result;
      Assert.IsTrue(o is null);
   }
}
