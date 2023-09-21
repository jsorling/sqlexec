using System;

namespace sqlExecTests.runner;

[TestClass]
public class EnumTests
{
   public enum EnumEnum
   {
      One = 1, Two = 2, Three = 3, Four = 4, Five = 5
   }

   public record Enum1Result(EnumEnum AnInt, string AString) : SqlExecBaseResult;

   public record Enum1Command(EnumEnum AnInt, string AString) : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.StoredProcedure;

      public override string SqlExecSqlText => "testssqlexec.AnIntAString2";
   }

   [TestMethod]
   public void MapEnum() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Enum1Result> res = runner.Query<Enum1Result, Enum1Command>(new(EnumEnum.One, "2"));
      Assert.IsTrue(res.First().AnInt == EnumEnum.One && res.First().AString == "2");
   }

   public enum SqlObjectGroupFlags
   {
      Tables = 1,
      Views = 2,
      StoredProcedures = 4,
      Functions = 8,
      TableTypes = 16,
      All = int.MaxValue
   }

   public record SqlObjectListItem(string Name, int ObjectId, string SchemaName, string DBName, string Group
      , SqlObjectGroupFlags GroupFlag) : SqlExecBaseResult;

   public record SqlObjectListCmd(string? Schema = null, SqlObjectGroupFlags Filter = SqlObjectGroupFlags.All) : ScriptResourceCommand
   {
      protected override string SqlResourceText => "sqlExecTests.scripts.objects.sql";

      protected override Type AssemblyType => typeof(SqlObjectListCmd);
   }

   [TestMethod]
   public void GetObjects() {
      IEnumerable<SqlObjectListItem>? res
         = new SqlExecRunner(TestsInitialize.ConnectionString).Query<SqlObjectListItem, SqlObjectListCmd>(
            new(null, SqlObjectGroupFlags.StoredProcedures | SqlObjectGroupFlags.Tables));
      Assert.IsTrue(res?.Any());
   }

   public enum EnumLong : long
   {
      One = 1, Two = 2, Three = 3, Four = 4, More = 65536
   }

   public record EnumLongResult(EnumLong EnumLong) : SqlExecBaseResult;

   public record EnumLongCommand() : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select cast(65536 as bigint)";
   }

   [TestMethod]
   public void MapEnumLong() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<EnumLongResult> res = runner.Query<EnumLongResult, EnumLongCommand>(new());
      Assert.IsTrue(res.First().EnumLong == EnumLong.More);
   }
}
