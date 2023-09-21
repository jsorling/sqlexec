if not exists ( select  *
   from    sys.schemas where   name = N'testssqlexec' ) 
      exec('create schema [testssqlexec]');
GO

create or alter procedure testssqlexec.AnIntAString
as begin
    select 1, '2'
end
GO

create or alter procedure testssqlexec.AnIntAString2
    @anint int,
    @astring nvarchar (65)
as begin
    select @anint, @astring
end
GO

create or alter procedure testssqlexec.AnIntAString3
    @anint int,
    @astring nvarchar (65)
as begin
    select @anint, @astring
    return 8
end
GO

create or alter procedure testssqlexec.SessionCtx
    @key nvarchar (65)
as begin
    select cast(session_context(@key) as nvarchar(65)) SessionValue
end
GO