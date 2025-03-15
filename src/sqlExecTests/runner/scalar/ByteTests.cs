namespace sqlExecTests.runner.scalar;

[TestClass]
public class ByteTests
{
   public record ByteCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(3 as tinyint)";
   }

   [TestMethod]
   public void ByteScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte? o = runner.ExecuteScalar<byte?, ByteCommand>(new());
      Assert.AreEqual(3, o);
   }

   [TestMethod]
   public void ByteNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte o = runner.ExecuteScalar<byte, ByteCommand>(new());
      Assert.AreEqual(3, o);
   }

   public record NullByteCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(null as tinyint)";
   }

   [TestMethod]
   public void NullByteScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte? o = runner.ExecuteScalar<byte?, NullByteCommand>(new());
      Assert.IsNull(o);
   }

   [TestMethod]
   public void ByteScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte? o = runner.ExecuteScalarAsync<byte?, ByteCommand>(new()).Result;
      Assert.AreEqual(3, o);
   }

   [TestMethod]
   public void NullByteScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte? o = runner.ExecuteScalarAsync<byte?, NullByteCommand>(new()).Result;
      Assert.IsNull(o);
   }
}
