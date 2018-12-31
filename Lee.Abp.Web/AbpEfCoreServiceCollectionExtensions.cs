using System;
using Abp.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lee.Abp.Web
{
    public static class AbpEfCoreServiceCollectionExtensions
    {
        public static void AddAbpDbContextEx<TDbContext>(
            this IServiceCollection services,
            Action<AbpDbContextConfiguration<TDbContext>> action)
            where TDbContext : DbContext
        {
            services.AddSingleton<IAbpDbContextConfigurer<TDbContext>>(
                sp => new AbpDbContextConfigurerActionEx<TDbContext>(sp, action)
            );
        }

        public static void AddAbpDbContext2<TDbContext>(
            this IServiceCollection services,
            Func<IServiceProvider, Action<AbpDbContextConfiguration<TDbContext>>> func)
            where TDbContext : DbContext
        {
            services.AddSingleton<IAbpDbContextConfigurer<TDbContext>>(sp => new AbpDbContextConfigurerAction<TDbContext>(func(sp)));
        }

    }
}
