using System;

namespace sqlExecTests.mapper;

[TestClass]
public class ScriptResourceTests
{
   public record ResCommand() : ScriptResourceCommand
   {
      protected override string SqlResourceText => "sqlExecTests.scripts.resourcecmd.sql";

      protected override Type AssemblyType => typeof(ResCommand);
   }

   [TestMethod]
   public void ResourceScalar() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      int? i = runner.ExecuteScalar<int?, ResCommand>(new());
      Assert.IsTrue(i == 1);
   }
}
