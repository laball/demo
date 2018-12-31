
默认初始化
ComponentNotFoundException: No component for supporting the service Abp.AspNetCore.Configuration.AbpAspNetCoreConfiguration was found
初始化问题；

使用AbpBootstrapper初始化
No component for supporting the service Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPartManager was found
初始化问题；


'Lee.Abp.Core.Users.Services.UserManager' is waiting for the following dependencies:
- Service 'Abp.Domain.Repositories.IRepository`1[[Lee.Abp.Core.Users.User, Lee.Abp.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]' which was not registered.
see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2548
    http://www.cnblogs.com/dj258/p/6600513.html
使用ABP，必须写DbSet，否则报以上错误；


The 'MySQLNumberTypeMapping' does not support value conversions. Support for value conversions typically requires changes in the database provider. 
see https://github.com/aspnet/EntityFrameworkCore/issues/11078
 MySQL provider is currently not working with EF Core 2.1
 老外有很多讨论，最终改的结论是原来的MySql驱动在EF Core 2.1下有问题导致；
 此问题巨坑（VS中降低.net core版本引用会导致各种编译错误）；

 Abp.Domain.Uow.AbpDbConcurrencyException: Database operation expected to affect 1 row(s) but actually affected 0 row(s). 
 Data may have been modified or deleted since entities were loaded. 
 See http://go.microsoft.com/fwlink/?LinkId=527962 for information on understanding and handling optimistic concurrency exceptions. 
 原因：乐观锁字段在插入时未“赋值”（正常应该是数据库自行设置）
 由于MySql timestamp字段类型的默认值是“0000-00-00 00:00:00”，而.ner DateTime的默认值是“0001-01-01 0:00:00”
 两边无法匹配，导致报错，乐观锁字段需要设置“[DatabaseGenerated(DatabaseGeneratedOption.Computed)]”

 发现一个新的MySql驱动"Pomelo.EntityFrameworkCore.MySql"，
 结果直接上.net core就有问题，明明没有表，自己还创建了，居然报错表已经存在,
 EF表“__efmigrationshistory”也没有插入数据，似乎问题很大，暂停探索；
 see https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql
 不过，这个GIT上，还有很多其他的库，有时间可以探索一下；


 ABP集成列表
 Abp.AutoMapper
 Abp.Dapper
 Abp.Castle.Log4Net
 Abp.HangFire.AspNetCore
 Abp.Quartz
 Abp.RedisCache
 Abp.TestBase

