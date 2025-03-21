﻿using Sorling.SqlExec.mapper;
using System;
using Microsoft.Data.SqlClient;

namespace sqlExecTests.mapper;

[TestClass]
public class SketchTests
{
   public record AnintAstring(int AnInt, string Astring) : SqlExecBaseResult;

   public record ACommand(int AnInt, string BString) : SqlExecBaseCommand
   {
      public override CommandType SqlExecCommandType => CommandType.Text;

      public override string SqlExecSqlText => "select @AnInt, @BString";
   }

   [TestMethod]
   public void MapRow() {
      using SqlConnection con = TestsInitialize.Connection;
      using SqlCommand com2 = con.CreateCommand();

      ACommand com3 = new(1, "2");
      foreach (MapProperty<ACommand>? x in MapCommand<ACommand>.CommandProperties) {
         object? y = x.Value(com3);
         string z = y.GetType().Name;
         string s = com3.SqlExecCommandType.ToString();
         string t = com3.SqlExecSqlText.ToString();
         Console.WriteLine(y.ToString());
         Console.WriteLine(z);
         Console.WriteLine(s);
         Console.WriteLine(t);
      }

      com2.CommandText = com3.SqlExecSqlText;
      com2.CommandType = com3.SqlExecCommandType;
      foreach (MapProperty<ACommand> par in MapCommand<ACommand>.CommandProperties) {
         _ = com2.Parameters.AddWithValue("@" + par.PropertyName!, par.Value(com3));
      }

      con.Open();
      using SqlDataReader reader2 = com2.ExecuteReader();
      AnintAstring? aa2 = reader2.ReadRecordSetAsync<AnintAstring>().Result.FirstOrDefault();
      Assert.AreEqual(1, aa2!.AnInt);
   }
}
