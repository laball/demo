
PG .net 官方文档：http://www.npgsql.org/doc/index.html
PG .net 类型对应表：http://www.npgsql.org/doc/types/basic.html

PG EF Code First
see:http://zacg.github.io/blog/2016/06/04/postgres-and-entity-framework-code-first/
see:http://www.cnblogs.com/znlgis/p/3952673.html

需要注意Npgsql和System.Threading.Tasks.Extensions的版本，否则可能会报文件加载失败；

The term 'update-database' is not recognized as the name of a cmdlet, function, script file, or operable program. Check the spelling of the name, or if a path was included, verify that the path is correct and try again.
重启VS，参见：https://stackoverflow.com/questions/9674983/the-term-update-database-is-not-recognized-as-the-name-of-a-cmdlet

PG默认值
see:https://www.postgresql.org/message-id/000c01c427b9$881dbda0$030a640a@tealuxe.com
see:https://www.postgresql.org/docs/9.1/static/ddl-default.html
不支持 ON UPDATE CURRENT_TIMESTAMP，只能使用触发器；

ABP多数据库集成：
https://stackoverflow.com/questions/49243891/how-to-use-multiples-databases-in-abp-core-zero/49248977#49248977