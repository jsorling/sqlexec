using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sqlExecTests.runner.scalar.ByteTests;

namespace sqlExecTests.mapper.mapcommand;

[TestClass]
public class EnumTests
{
   enum SqlGroupFlags : long
   {
      Tables = 1,
      Views = 2,
      StoredProcedures = 4,
      Functions = 8,
      TableTypes = 16,
      ResultSet = 32,
      Types = 64,
      DefaultConstraints = 128,
      CheckConstraints = 256,
      DbRoles = 512,
      AppRoles = 1024,
      UserCertificates = 2048,
      ExtUsersAzureAD = 4096,
      WinGroups = 8192,
      UserAsymetricKeys = 16384,
      SqlUsers = 32768,
      WinUsers = 65536,
      ExtGroupsAzureAD = 131072,
      Triggers = 262144,
      Schemas = 524288,
      Principals = DbRoles
         | AppRoles
         | ExtUsersAzureAD
         | UserCertificates
         | WinGroups
         | UserAsymetricKeys
         | SqlUsers
         | WinUsers
         | ExtGroupsAzureAD,
      Objects = ~0
   }

   record EnumCmd(SqlGroupFlags Flags) : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text; 

      public override string SqlExecSqlText => "select @flags";
   }

   record EnumNullCmd(SqlGroupFlags? Flags) : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select @flags";
   }

   [TestMethod]
   public void MapEnum() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      SqlGroupFlags o = runner.ExecuteScalar<SqlGroupFlags, EnumCmd>(new(SqlGroupFlags.Principals));
      Assert.IsTrue(o == SqlGroupFlags.Principals);
   }

   [TestMethod]
   public void MapEnumNull() {
      SqlExecRunner runner = new(TestsInitialize.ConnectionString);
      SqlGroupFlags? o = runner.ExecuteScalar<SqlGroupFlags?, EnumNullCmd>(new(null));
      Assert.IsTrue(o is null);
      o = runner.ExecuteScalar<SqlGroupFlags?, EnumNullCmd>(new(SqlGroupFlags.AppRoles));
      Assert.IsTrue(o == SqlGroupFlags.AppRoles);
   }
}
