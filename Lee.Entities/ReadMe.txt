EF Code First With MySQL

field mapping(default):

//****************************************
bool    => tinyint(1)
byte    => tinyint(3) UNSIGNED
short   => smallint(6)
int     => int(11)
enum    => int(11)
long    => bigint(20)
decimal => decimal(18,2)
float   => float
double  => double
//****************************************

//******************************************************************************
string with length setting => varchar(length)
ex:
[StringLength(20)] => varchar(20)

string with max and min length setting => varchar(max length) not null
 ex:
[MaxLength(20), MinLength(10)] => varchar(20) not null

string without length setting => longtext

Guid => char(36)

//******************************************************************************




字段控制：


ef migrations command
see http://www.cnblogs.com/jinzhao/archive/2012/08/13/2636747.html

1 Enable-Migrations
Syntax
Enable-Migrations [-EnableAutomaticMigrations] [[-ProjectName] <String>]
  [-Force] [<CommonParameters>]

2 Add-Migration
Add-Migration [-Name] <String> [-Force]
  [-ProjectName <String>] [-StartUpProjectName <String>]
  [-ConfigurationTypeName <String>] [-ConnectionStringName <String>]
  [-IgnoreChanges] [<CommonParameters>]
 
Add-Migration [-Name] <String> [-Force]
  [-ProjectName <String>] [-StartUpProjectName <String>]
  [-ConfigurationTypeName <String>] -ConnectionString <String>
  -ConnectionProviderName <String> [-IgnoreChanges] [<Common Parameters>]

  3 Update-Database
  Syntax
Update-Database [-SourceMigration <String>]
  [-TargetMigration <String>] [-Script] [-Force] [-ProjectName <String>]
  [-StartUpProjectName <String>] [-ConfigurationTypeName <String>]
  [-ConnectionStringName <String>] [<CommonParameters>]
 
Update-Database [-SourceMigration <String>] [-TargetMigration <String>]
  [-Script] [-Force] [-ProjectName <String>] [-StartUpProjectName <String>]
  [-ConfigurationTypeName <String>] -ConnectionString <String>
  -ConnectionProviderName <String> [<CommonParameters>]
