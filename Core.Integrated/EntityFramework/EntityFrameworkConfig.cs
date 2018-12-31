using Autofac;
using Core.Dao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Integrated
{
    public static class EntityFrameworkConfig
    {
        public static IServiceCollection UseEF(this IServiceCollection services, string connectionString)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            //使用默认模式设置EF
            //services.AddDbContext<DemoDbContext>(options =>
            //{
            //    options.UseMySQL(config.GetConnectionString("DefaultConnection"));
            //});

            //使用池化方式设置EF
            //see https://docs.microsoft.com/zh-cn/ef/core/what-is-new/ef-core-2.0
            //    https://stackoverflow.com/questions/48443567/adddbcontext-or-adddbcontextpool
            services.AddDbContextPool<DemoDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });

            //升级.net core直接使用子类是可以的，没有报错；
            services.AddScoped(c => c.GetService<DemoDbContext>() as DbContext);

            return services;
        }

        public static void UseEF(this ContainerBuilder builder, IConfiguration config)
        {
            //注册泛型仓储
            builder.RegisterGeneric(typeof(EFRepository<>))
                .As(typeof(IRepository<>))
                .As(typeof(EFRepository<>))
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.Register(c =>
            {
                var optionBuilder = new DbContextOptionsBuilder<DemoDbContext>();
                optionBuilder.UseMySQL(config.GetConnectionString("DefaultConnection"));
                return optionBuilder.Options;
            }).InstancePerLifetimeScope();

            builder.Register(contex =>
            {
                return new DemoDbContext(contex.Resolve<DbContextOptions<DemoDbContext>>());
            }).InstancePerLifetimeScope();
        }

    }
}
