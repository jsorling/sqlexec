namespace sqlExecTests.mapper;

[TestClass]
public class AttributeTests
{
   public record Attr1Result(int AnInt, string AString) : SqlExecBaseResult;

   public record Attr1Command([property: SqlSource("anint")] int XInt, string AString) : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.StoredProcedure;

      public override string SqlExecSqlText => "testssqlexec.AnIntAString2";
   }

   [TestMethod]
   public void ParmeterName() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      IEnumerable<Attr1Result> res = runner.Query<Attr1Result, Attr1Command>(new(1, "2"));
      Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
   }
}
