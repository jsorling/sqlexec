--tables
select t.name[Name], t.object_id[ObjectId], s.name[SchemaName], 'Table' [Group], db_name() [DBName], cast(1 as int) [GroupFlag]
from sys.tables t inner join sys.schemas s on s.schema_id = t.schema_id 
where (( @schema is not null and s.name = @schema ) or @schema is null) and 1 & @filter <> 0 or @filter = 0
union all
--views
select t.name[Name], t.object_id[ObjectId], s.name[SchemaName], 'View' [Group], db_name() [DBName], cast(2 as int) [GroupFlag]
from sys.views t inner join sys.schemas s on s.schema_id = t.schema_id 
where (( @schema is not null and s.name = @schema ) or @schema is null)  and 2 & @filter <>
0
union all
--stored procedures
select t.name[Name], t.object_id[ObjectId], s.name[SchemaName], 'Stored procedure' [Group], db_name() [DBName], cast(4 as int) [GroupFlag]
from sys.procedures t inner join sys.schemas s on s.schema_id = t.schema_id
where (( @schema is not null and s.name = @schema ) or @schema is null)  and 4 & @filter <> 0
union all
--functions
select t.name[Name], t.object_id[ObjectId], s.name[SchemaName], 'Function' [Group], db_name() [DBName], cast(8 as int) [GroupFlag]
from sys.objects t inner join sys.schemas s on s.schema_id = t.schema_id 
where t.type in ( 'FN', 'IF', 'TF', 'FS', 'FT' ) and ( ( @schema is not null and s.name = @schema ) or @schema is null )  and 8 & @filter <> 0
union all
--table types
select t.name[Name], t.type_table_object_id[ObjectId], s.name[SchemaName], 'Table type' [Group], db_name() [DBName], cast(16 as int) [GroupFlag]
from sys.table_types t inner join sys.schemas s on s.schema_id = t.schema_id 
where (( @schema is not null and s.name = @schema ) or @schema is null)  and 16 & @filter <> 0

order by s.Name, t.name