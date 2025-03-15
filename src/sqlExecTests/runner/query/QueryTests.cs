using Sorling.SqlExec.mapper;
using System;

namespace sqlExecTests.runner.query;

[TestClass]
public class QueryTests
{
   public record Query1Result(int AnInt, string AString) : SqlExecBaseResult;

   public record Query1Command() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select 1, '2'";
   }

   [TestMethod]
   public void Query1() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query1Result> res = runner.Query<Query1Result, Query1Command>(new());
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }

   [TestMethod]
   public void Query1Async() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query1Result> res = runner.QueryAsync<Query1Result, Query1Command>(new()).Result;
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }

   [TestMethod]
   public void Query1FirstRow() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Query1Result? res = runner.QueryFirstRow<Query1Result, Query1Command>(new());
      Assert.IsTrue(res?.AnInt == 1 && res?.AString == "2");
   }

   [TestMethod]
   public void Query1FirstRowAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Query1Result? res = runner.QueryFirstRowAsync<Query1Result, Query1Command>(new()).Result;
      Assert.IsTrue(res?.AnInt == 1 && res?.AString == "2");
   }

   public record Query2Result(int AnInt, string AString) : SqlExecBaseResult;

   public record Query2Command(int AnInt, string AString) : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select @anint, @astring";
   }

   [TestMethod]
   public void Query2() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query2Result> res = runner.Query<Query2Result, Query2Command>(new(1, "2"));
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }

   [TestMethod]
   public void Query2FirstRow() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Query2Result? res = runner.QueryFirstRow<Query2Result, Query2Command>(new(1, "2"));
      Assert.IsTrue(res?.AnInt == 1 && res?.AString == "2");
   }

   [TestMethod]
   public void Query2Async() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query2Result> res = runner.QueryAsync<Query2Result, Query2Command>(new(1, "2")).Result;
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }

   [TestMethod]
   public void Query2FirstRowAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      Query2Result? res = runner.QueryFirstRowAsync<Query2Result, Query2Command>(new(1, "2")).Result;
      Assert.IsTrue(res?.AnInt == 1 && res?.AString == "2");
   }

   public record Query3Command(int AnInt, string AString) : SqlExecBaseCommand
   {
      public record Query3Result(int AnInt, string AString) : SqlExecBaseResult;

      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select 1, cast(null as varchar(65))";
   }

   [TestMethod]
   public void Query3() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      SqlFieldMapperNonNullableException ex = Assert.ThrowsException<SqlFieldMapperNonNullableException>(
         () => runner.Query<Query3Command.Query3Result, Query3Command>(new(1, "2")));

      Console.WriteLine(ex.Message);
   }

   public record Query4Command(int AnInt, string AString) : SqlExecBaseCommand
   {
      public record Query4Result(int AnInt, string? AString) : SqlExecBaseResult;

      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select 1, cast(null as varchar(65))";
   }

   [TestMethod]
   public void Query4() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Query4Command.Query4Result> res
         = runner.QueryAsync<Query4Command.Query4Result, Query4Command>(new(1, "2")).Result;
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString is null);
   }

   public record QueryNullParameterCommand(int? AnInt) : SqlExecBaseCommand
   {
      public record QueryNullParameterResult(int AnInt) : SqlExecBaseResult;

      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select coalesce(@anint, 1)";
   }

   [TestMethod]
   public void QueryNullParameter() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<QueryNullParameterCommand.QueryNullParameterResult> res
         = runner.QueryAsync<QueryNullParameterCommand.QueryNullParameterResult, QueryNullParameterCommand>(new(null)).Result;
      Assert.AreEqual(1, res.First().AnInt);

      res = runner.QueryAsync<QueryNullParameterCommand.QueryNullParameterResult, QueryNullParameterCommand>(new(3)).Result;
      Assert.AreEqual(3, res.First().AnInt);
   }
}
