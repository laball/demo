using System;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using Castle.Facilities.Logging;
using Core.Integrated;
using Hangfire;
using Hangfire.MemoryStorage;
using Lee.Abp.EntityFrameworkCore;
using Lee.Abp.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Lee.Abp.Web.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // 在ABP中动态生成的API需要访问HttpContext时，无法直接使用该类
            // 与ASP.NET设计不同，HttpContext没有静态变量可以访问；
            // git上的相关讨论：
            // https://github.com/aspnet/Announcements/issues/190
            // https://github.com/aspnet/Hosting/issues/793
            // 注册时需要注册为单例模式；
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.EnableCors()
               .EnableVersion()
               .UseSwagger();

            services.AddSingleton<IDbResolve, DbResolve>();

            // 此处，需要重载AddAbpDbContext扩展方法，方法，必须有IServiceProvider参数，利用IServiceProvider
            // 从中心数据库中获取各个租户的数据库连接信息；
            services.AddAbpDbContextEx<LeeAbpDbContext>(options =>
            {

            });

            //services.AddAbpDbContext2<LeeAbpDbContext>(
            //    sp =>
            //    {
            //        return options =>
            //        {
            //            IDbResolve dbResolve = (IDbResolve)sp.GetService(typeof(IDbResolve));
            //            options.DbContextOptions.UseMySQL(dbResolve.GetConnectionString());
            //        };
            //    }
            //);

            services.AddMvc().AddControllersAsServices();
            services.AddMvcCore().AddApiExplorer();

            // ******************************************************************
            // 集成Hangfire
            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();

                // Hangfire.MySql.Core 
                // You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near '(6) NOT NULL,
                // `ExpireAt` datetime(6) DEFAULT NULL,
                // PRIMARY KEY(`Id`),
                // KEY' at line 10
            });

            // ******************************************************************

            return services.AddAbp<LeeAbpWebModule>(options =>
            {
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticHttpContext();

            //app.UseMiddleware<AbpMiddleware>();
            app.UseMiddleware<MockUserLogInMiddleware>();

            app.UseAbp();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
            app.ConfigSwaggerUI();

            //******************************************************************
            //see https://blog.csdn.net/WuLex/article/details/78454519
            //    https://blog.csdn.net/anyusheng/article/details/78672965
            //    http://www.cnblogs.com/1zhk/p/5438838.html
            //集成Hangfire
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new AbpHangfireAuthorizationFilter() }
            //});
            //******************************************************************

        }
    }
}
