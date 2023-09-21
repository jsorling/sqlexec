using System;

namespace sqlExecTests.runner.query;

[TestClass]
public class QueryMultiSetsTests
{
   public record Query1Result(int AnInt, string AString) : SqlExecBaseResult;

   public record Query1Command() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select 1, '2';select 2, '2';select 3, '2';select 4, '2';select 5, '2';select 6, '2';select 7, '2';select 8, '2';";
   }

   public record Query2Command() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select 1, '2';select 2, '2';select 3, '2';select 4, '2';select 5, '2';";
   }

   [TestMethod]
   public void TwoSetsAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2)
         = runner.QueryAsync<Query1Result, Query1Result, Query1Command>(new()).Result;
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2);
   }

   [TestMethod]
   public void ThreeSetsAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3)
         = runner.QueryAsync<Query1Result, Query1Result, Query1Result, Query1Command>(new()).Result;
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3);
   }

   [TestMethod]
   public void FourSetsAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3, IEnumerable<Query1Result> res4)
         = runner.QueryAsync<Query1Result, Query1Result, Query1Result, Query1Result, Query1Command>(new()).Result;
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3 && res4.First().AnInt == 4);
   }

   [TestMethod]
   public void FiveSetsAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3, IEnumerable<Query1Result> res4, IEnumerable<Query1Result> res5)
         = runner.QueryAsync<Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Command>(new()).Result;
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3 && res4.First().AnInt == 4 && res5.First().AnInt == 5);
   }

   [TestMethod]
   public void SixSetsAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3, IEnumerable<Query1Result> res4, IEnumerable<Query1Result> res5, IEnumerable<Query1Result> res6)
         = runner.QueryAsync<Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Command>(new()).Result;
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3 && res4.First().AnInt == 4 && res5.First().AnInt == 5 && res6.First().AnInt == 6);
   }

   [TestMethod]
   public void SevenSetsAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3, IEnumerable<Query1Result> res4, IEnumerable<Query1Result> res5, IEnumerable<Query1Result> res6, IEnumerable<Query1Result> res7)
         = runner.QueryAsync<Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Command>(new()).Result;
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3 && res4.First().AnInt == 4 && res5.First().AnInt == 5 && res6.First().AnInt == 6 && res7.First().AnInt == 7);
   }

   [TestMethod]
   public void EightSetsAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3, IEnumerable<Query1Result> res4, IEnumerable<Query1Result> res5, IEnumerable<Query1Result> res6, IEnumerable<Query1Result> res7, IEnumerable<Query1Result> res8)
         = runner.QueryAsync<Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Command>(new()).Result;
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3 && res4.First().AnInt == 4 && res5.First().AnInt == 5 && res6.First().AnInt == 6 && res7.First().AnInt == 7 && res8.First().AnInt == 8);
   }

   [TestMethod]
   public void TooFewSetsAsync() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      _ = Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _ = runner.QueryAsync<Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query2Command>(new()));

   }

   [TestMethod]
   public void TwoSets() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2)
         = runner.Query<Query1Result, Query1Result, Query1Command>(new());
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2);
   }

   [TestMethod]
   public void ThreeSets() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3)
         = runner.Query<Query1Result, Query1Result, Query1Result, Query1Command>(new());
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3);
   }

   [TestMethod]
   public void FourSets() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3, IEnumerable<Query1Result> res4)
         = runner.Query<Query1Result, Query1Result, Query1Result, Query1Result, Query1Command>(new());
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3 && res4.First().AnInt == 4);
   }

   [TestMethod]
   public void FiveSets() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3, IEnumerable<Query1Result> res4, IEnumerable<Query1Result> res5)
         = runner.Query<Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Command>(new());
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3 && res4.First().AnInt == 4 && res5.First().AnInt == 5);
   }

   [TestMethod]
   public void SixSets() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3, IEnumerable<Query1Result> res4, IEnumerable<Query1Result> res5, IEnumerable<Query1Result> res6)
         = runner.Query<Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Command>(new());
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3 && res4.First().AnInt == 4 && res5.First().AnInt == 5 && res6.First().AnInt == 6);
   }

   [TestMethod]
   public void SevenSets() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3, IEnumerable<Query1Result> res4, IEnumerable<Query1Result> res5, IEnumerable<Query1Result> res6, IEnumerable<Query1Result> res7)
         = runner.Query<Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Command>(new());
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3 && res4.First().AnInt == 4 && res5.First().AnInt == 5 && res6.First().AnInt == 6 && res7.First().AnInt == 7);
   }

   [TestMethod]
   public void EightSets() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      (IEnumerable<Query1Result> res1, IEnumerable<Query1Result> res2, IEnumerable<Query1Result> res3, IEnumerable<Query1Result> res4, IEnumerable<Query1Result> res5, IEnumerable<Query1Result> res6, IEnumerable<Query1Result> res7, IEnumerable<Query1Result> res8)
         = runner.Query<Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Command>(new());
      Assert.IsTrue(res1.First().AnInt == 1 && res2.First().AnInt == 2 && res3.First().AnInt == 3 && res4.First().AnInt == 4 && res5.First().AnInt == 5 && res6.First().AnInt == 6 && res7.First().AnInt == 7 && res8.First().AnInt == 8);
   }

   [TestMethod]
   public void TooFewSets() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      _ = Assert.ThrowsException<InvalidOperationException>(() => _ = runner.Query<Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query1Result, Query2Command>(new()));

   }
}
