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
      Assert.IsTrue(o == 3);
   }

   [TestMethod]
   public void ByteNonNullableScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte o = runner.ExecuteScalar<byte, ByteCommand>(new());
      Assert.IsTrue(o == 3);
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
      Assert.IsTrue(o is null);
   }

   [TestMethod]
   public void ByteScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte? o = runner.ExecuteScalarAsync<byte?, ByteCommand>(new()).Result;
      Assert.IsTrue(o == 3);
   }

   [TestMethod]
   public void NullByteScalarAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      byte? o = runner.ExecuteScalarAsync<byte?, NullByteCommand>(new()).Result;
      Assert.IsTrue(o is null);
   }
}
