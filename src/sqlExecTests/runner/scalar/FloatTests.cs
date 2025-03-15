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
      Assert.AreEqual(8, (int)o!.Value);
   }

   [TestMethod]
   public void FloatNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      float o = runner.ExecuteScalar<float, FloatCommand>(new());
      Assert.AreEqual(8, (int)o);
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
      Assert.IsNull(o);
   }

   [TestMethod]
   public void FloatScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      float? o = runner.ExecuteScalarAsync<float?, FloatCommand>(new()).Result;
      Assert.AreEqual(8, (int)o!.Value);
   }

   [TestMethod]
   public void NullFloatScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      float? o = runner.ExecuteScalarAsync<float?, NullFloatCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
