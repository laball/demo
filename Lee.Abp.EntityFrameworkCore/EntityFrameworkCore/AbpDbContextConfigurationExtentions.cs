using Abp.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Lee.Abp.EntityFrameworkCore
{
    public static class AbpDbContextConfigurationExtentions
    {
        public static void UseMySQL(this AbpDbContextConfiguration<LeeAbpDbContext> dbContextOptions)
        {
            //连接字符串在EF所在Module中设置；
            dbContextOptions.DbContextOptions.UseMySQL(dbContextOptions.ConnectionString);
        }

        /// <summary>
        /// 利用此方法从核心数据库中获取各个租户的数据库连接信息；
        /// </summary>
        /// <param name="dbContextOptions"></param>
        /// <param name="dbResolve"></param>
        public static void UseMySQL(this AbpDbContextConfiguration<LeeAbpDbContext> dbContextOptions, IDbResolve dbResolve)
        {
            //连接字符串在EF所在Module中设置；
            dbContextOptions.DbContextOptions.UseMySQL(dbResolve.GetConnectionString());
        }

    }
}