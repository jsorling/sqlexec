namespace sqlExecTests.runner;

[TestClass]
public class DynamicCommandTests
{
   [TestMethod]
   public void DynScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      bool? o = runner.ExecuteScalar<bool?, ScriptDynamicSqlCommand>(new("select cast(1 as bit)"));
      Assert.IsTrue(o);
   }
}
