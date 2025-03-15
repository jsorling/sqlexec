namespace sqlExecTests.runner.sqlsessionctx;

[TestClass]
public class SqlSessionCtxTests
{
   public record StringCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(session_context(N'SqlSessionCtxRunner') as nvarchar(10));";
   }

   [TestMethod]
   public void GetSessionScalar() {
      SqlSessionCtxRunner runner = new(TestsInitialize.ConnectionString, new() { { "SqlSessionCtxRunner", "1005" } });
      string? o = runner.ExecuteScalar<string, StringCommand>(new());
      Assert.AreEqual("1005", o);
   }

   [TestMethod]
   public void GetSessionScalarAsync() {
      SqlSessionCtxRunner runner = new(TestsInitialize.ConnectionString, new() { { "SqlSessionCtxRunner", "1005" } });
      string? o = runner.ExecuteScalarAsync<string, StringCommand>(new()).Result;
      Assert.AreEqual("1005", o);
   }

   public record SessionCtxCmd([SqlSource("@key", "nvarchar(130) null ", "0")] string? Key) : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.StoredProcedure;

      public override string SqlExecSqlText => "testssqlexec.SessionCtx";

      [ResultSetRow(0)]
      public record ResultSet0(
        [SqlSource("SessionValue", "nvarchar(65) null ", "0")] string? SessionValue
      ) : SqlExecBaseResult;
   }

   [TestMethod]
   public void GetSessionScalaSPAsync() {
      SqlSessionCtxRunner runner = new(TestsInitialize.ConnectionString, new() { { "SqlSessionCtxRunner", "1005" } });
      SessionCtxCmd.ResultSet0? res = runner.QueryFirstRow<SessionCtxCmd.ResultSet0, SessionCtxCmd>(new("SqlSessionCtxRunner"));
      Assert.AreEqual("1005", res!.SessionValue);
   }
}
