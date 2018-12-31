1、IOC Autofac 集成 ✔；
2、API Doc Swagger 集成 ✔；
3、AOP Castle.Core 集成 ✔；
4、ORM EF 集成 ✔；
5、ORM NHibernate 集成 ✔；
6、ORM Dapper 集成 ✔；
7、Distributed Cache Redis 集成 ✔；
8、object-object mapper AutoMapper 集成 ✔；
9、ESB MassTransit 集成 ✔；
10、MQ RabbitMQ 集成 ✔；
11、Job Scheduler Quartz.NET 集成 ✔；
12、Http Client Refit 集成 ✔；
13、Distributed Virtual Actor Model Orleans 集成 ✔；



The host 10.27.225.165 does not support SSL connections.
see https://blog.csdn.net/u010388954/article/details/80136882

Unable to cast object of type 'System.String' to type 'System.Linq.Expressions.LambdaExpression'.
原因是DemoDbContext中OnModelCreating函数内，使用HasQueryFilter时，使用了 [ 非常量 ] string.Empty。