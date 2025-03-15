namespace sqlExecTests.runner;

[TestClass]
public class ExecuteTests
{
   public record Exec1Command() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select 1, '2'";
   }

   [TestMethod]
   public void Execute1() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int i = runner.Execute<Exec1Command>(new());
      Assert.AreEqual(-1, i);
   }

   [TestMethod]
   public void Execute1Async() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int i = runner.ExecuteAsync<Exec1Command>(new()).Result;
      Assert.AreEqual(-1, i);
   }

   public record Exec2Command() : ScriptFileCommand
   {
      public override string SqlExecFilePath => ".\\scripts\\Execute2.sql";
   }

   [TestMethod]
   public void Execute2() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int i = runner.Execute<Exec2Command>(new());
      Assert.AreEqual(-1, i);
   }

   [TestMethod]
   public void Execute2Async() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int i = runner.ExecuteAsync<Exec2Command>(new()).Result;
      Assert.AreEqual(-1, i);
   }

   public record Exec3Command() : ScriptFileCommand
   {
      public override string SqlExecFilePath => ".\\scripts\\CreateTestDBObjects.sql";
   }

   [TestMethod]
   public void Execute3() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int i = runner.ExecuteGOScript<Exec3Command>(new());
      Assert.AreEqual(5, i);
   }

   [TestMethod]
   public void Execute3Async() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int i = runner.ExecuteGOScriptAsync<Exec3Command>(new()).Result;
      Assert.AreEqual(5, i);
   }

   public record Query4Result(int AnInt, string AString) : SqlExecBaseResult;

   public record Query4Command() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.StoredProcedure;

      public override string SqlExecSqlText => "testssqlexec.AnIntAString";
   }

   [TestMethod]
   public void Execute4() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query4Result>? res = runner.Query<Query4Result, Query4Command>(new());
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }

   [TestMethod]
   public void Execute4FirstRow() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Query4Result? res = runner.QueryFirstRow<Query4Result, Query4Command>(new());
      Assert.IsTrue(res!.AnInt == 1 && res!.AString == "2");
   }

   [TestMethod]
   public void Execute4Async() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query4Result> res = runner.QueryAsync<Query4Result, Query4Command>(new()).Result;
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }

   [TestMethod]
   public void Execute4FirstRowAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Query4Result? res = runner.QueryFirstRowAsync<Query4Result, Query4Command>(new()).Result;
      Assert.IsTrue(res!.AnInt == 1 && res!.AString == "2");
   }

   public record Query5Result(int AnInt, string AString) : SqlExecBaseResult;

   public record Query5Command(int AnInt, string AString) : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.StoredProcedure;

      public override string SqlExecSqlText => "testssqlexec.AnIntAString2";
   }

   [TestMethod]
   public void Execute5() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query5Result> res = runner.Query<Query5Result, Query5Command>(new(1, "2"));
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }

   [TestMethod]
   public void Execute5FirstRow() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Query5Result? res = runner.QueryFirstRow<Query5Result, Query5Command>(new(1, "2"));
      Assert.IsTrue(res!.AnInt == 1 && res!.AString == "2");
   }

   [TestMethod]
   public void Execute5Async() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query5Result> res = runner.QueryAsync<Query5Result, Query5Command>(new(1, "2")).Result;
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }

   [TestMethod]
   public void Execute5FirstRowAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Query5Result? res = runner.QueryFirstRowAsync<Query5Result, Query5Command>(new(1, "2")).Result;
      Assert.IsTrue(res!.AnInt == 1 && res!.AString == "2");
   }

   public record Query6Result(int AnInt, string AString) : SqlExecBaseResult;

   public record Query6Command(int AnInt, string AString) : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.StoredProcedure;

      public override string SqlExecSqlText => "testssqlexec.AnIntAString3";
   }

   [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "SqlParameter")]
   public record Query6AtCommand(int @anint, string @astring) : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.StoredProcedure;

      public override string SqlExecSqlText => "testssqlexec.AnIntAString3";
   }

   [TestMethod]
   public void Execute6() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query6Result> res = runner.Query<Query6Result, Query6Command>(new(1, "2"));
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }

   [TestMethod]
   public void Execute6At() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query6Result> res = runner.Query<Query6Result, Query6AtCommand>(new(1, "2"));
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }

   [TestMethod]
   public void Execute6FirstRow() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Query6Result? res = runner.QueryFirstRow<Query6Result, Query6Command>(new(1, "2"));
      Assert.IsTrue(res!.AnInt == 1 && res!.AString == "2");
   }

   [TestMethod]
   public void Execute6Async() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query6Result> res = runner.QueryAsync<Query6Result, Query6Command>(new(1, "2")).Result;
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }

   [TestMethod]
   public void Execute6FirstRowAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Query6Command command = new(1, "2");
      Query6Result? res = runner.QueryFirstRowAsync<Query6Result, Query6Command>(command).Result;
      Assert.IsTrue(res!.AnInt == 1 && res!.AString == "2");
   }

   [TestMethod]
   public void Execute6ReturnValue() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Query6Command cmd = new(1, "2");
      int i = runner.Execute(cmd);
      Assert.IsTrue(i == -1 && cmd.SqlExecReturnValue == 8);
   }
}
