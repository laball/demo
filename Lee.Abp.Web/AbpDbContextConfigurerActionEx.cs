using System;
using Abp.EntityFrameworkCore.Configuration;
using Lee.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lee.Abp.Web
{
    public class AbpDbContextConfigurerActionEx<TDbContext> : IAbpDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        private readonly IServiceProvider serviceProvider;

        public Action<AbpDbContextConfiguration<TDbContext>> Action { get; set; }

        public AbpDbContextConfigurerActionEx(IServiceProvider serviceProvider, Action<AbpDbContextConfiguration<TDbContext>> action)
        {
            this.serviceProvider = serviceProvider;
            Action = action;
        }

        public void Configure(AbpDbContextConfiguration<TDbContext> configuration)
        {
            Action(configuration);

            IDbResolve dbResolve = (IDbResolve)serviceProvider.GetService(typeof(IDbResolve));

            configuration.DbContextOptions.UseMySQL(dbResolve.GetConnectionString());
        }
    }
}
