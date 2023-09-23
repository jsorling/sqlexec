# sqlexec
Sql server C# ORM
## Nuget source
https://pkgs.dev.azure.com/sorling/PublicFeed/_packaging/PublicFeed/nuget/v3/index.json
## Execute query
### Query from Sql statement
```C#
public record Query2Result(int AnInt, string AString) : SQLExecBaseResult;

public record Query2Command(int AnInt, string AString) : SQLExecBaseCommand {
    public override CommandType SQLExecCommandType => CommandType.Text;

    public override string SQLExecSQLText => "select @anint, @astring";
}

[TestMethod]
public void Query2() {
    SQLExecRunner runner = new(TestsInitialize.ConnectionString);
    IEnumerable<Query2Result> res = runner.Query<Query2Result, Query2Command>(new(1, "2"));
    Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
}

[TestMethod]
public void Query2FirstRow() {
    SQLExecRunner runner = new(TestsInitialize.ConnectionString);
    Query2Result? res = runner.QueryFirstRow<Query2Result, Query2Command>(new(1, "2"));
    Assert.IsTrue(res?.AnInt == 1 && res?.AString == "2");
}
```
### Query stored procedure
```C#
public record Query4Result(int AnInt, string AString) : SQLExecBaseResult;

public record Query4Command() : SQLExecBaseCommand {
    public override CommandType SQLExecCommandType => CommandType.StoredProcedure;

    public override string SQLExecSQLText => "testssqlexec.AnIntAString";
}

[TestMethod]
public void Execute4() {
    SQLExecRunner runner = new(TestsInitialize.ConnectionString);
    IEnumerable<Query4Result> res = runner.Query<Query4Result, Query4Command>(new());
    Assert.IsTrue(res.First().AnInt == 1 && res.First().AString == "2");
}

[TestMethod]
public void Execute4FirstRow() {
    SQLExecRunner runner = new(TestsInitialize.ConnectionString);
    Query4Result? res = runner.QueryFirstRow<Query4Result, Query4Command>(new());
    Assert.IsTrue(res!.AnInt == 1 && res!.AString == "2");
}
```
## Execute scalar
```C#
public record StringCommand() : SQLExecBaseCommand {
    public override CommandType SQLExecCommandType => CommandType.Text;

    public override string SQLExecSQLText => "select '123'";
}

[TestMethod]
public void StringScalar() {
    SQLExecRunner runner = new(TestsInitialize.ConnectionString);
    string? o = runner.ExecuteScalar<string, StringCommand>(new());
    Assert.IsTrue(o == "123");
}
```
### Scalar return values
Supported types:
- byte?
- short?
- int?
- long?
- float?
- double?
- decimal?
- bool?
- string
- char?
- Guid?
- DateTime?
- DateTimeOffset?
- TimeSpan?
- byte[]
- char[]

Other types throws new NotSupportedException().
