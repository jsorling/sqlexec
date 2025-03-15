namespace sqlExecTests.runner.scalar;

[TestClass]
public class BinaryTests
{
   public record BinaryCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast( 123456 as binary(4) )";
   }

   [TestMethod]
   public void BinaryScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte[]? o = runner.ExecuteScalar<byte[]?, BinaryCommand>(new());
      Assert.IsTrue(o?.Length > 0);
   }

   [TestMethod]
   public void BinaryNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      byte[] o = runner.ExecuteScalar<byte[], BinaryCommand>(new());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
      Assert.IsTrue(o?.Length > 0);
   }

   public record NullBinaryCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast( null as binary(4) )";
   }

   [TestMethod]
   public void NullBinaryScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte[]? o = runner.ExecuteScalar<byte[]?, NullBinaryCommand>(new());
      Assert.IsNull(o);
   }

   [TestMethod]
   public void BinaryScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte[]? o = runner.ExecuteScalarAsync<byte[]?, BinaryCommand>(new()).Result;
      Assert.IsTrue(o?.Length > 0);
   }

   [TestMethod]
   public void NullBinaryScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte[]? o = runner.ExecuteScalarAsync<byte[]?, NullBinaryCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
