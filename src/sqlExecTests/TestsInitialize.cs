using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

namespace sqlExecTests;

[TestClass]
public class TestsInitialize
{
   private static IConfigurationSection InitWithUserSecrets<T>(string section) where T : class
      => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json", true, false).AddUserSecrets<T>().Build().GetSection(section);

   public static SqlConnection Connection => new(ConnectionString);

   public static string ConnectionString { get; private set; } = "";

   public record CreateTestDBObjects() : ScriptFileCommand
   {
      public override string SqlExecFilePath => ".\\scripts\\CreateTestDBObjects.sql";
   }

   [AssemblyInitialize]
#pragma warning disable IDE0060 // Remove unused parameter
   public static void AssemblyInitialize(TestContext testContext) {
#pragma warning restore IDE0060 // Remove unused parameter
      IConfigurationSection conf = InitWithUserSecrets<TestsInitialize>("Test");
      ConnectionString = conf["ConnectionString"]
         ?? throw new ApplicationException("ConnectionString not set in configuration");

      SqlExecRunner runner = new(ConnectionString);
      _ = runner.ExecuteGOScript<CreateTestDBObjects>(new());
   }
}
