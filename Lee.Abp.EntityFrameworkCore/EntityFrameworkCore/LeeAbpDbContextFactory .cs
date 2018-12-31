using System.IO;
using Lee.Abp.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Lee.Abp.EntityFrameworkCore
{
    public class LeeAbpDbContextFactory : IDesignTimeDbContextFactory<LeeAbpDbContext>
    {
        /// <summary>
        /// ABP提供的示例代码是读取Web项目的配置文件
        /// 个人感觉不是很合适，采用共用配置文件的方式，似乎更好；
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

                var configuration = configBuilder.Build();
                return configuration.GetConnectionString();
            }
        }

        public LeeAbpDbContext CreateDbContext(string[] args)
        {
            return new LeeAbpDbContext(BuildOptions());
        }

        public static DbContextOptions<LeeAbpDbContext> BuildOptions()
        {
            var builder = new DbContextOptionsBuilder<LeeAbpDbContext>();
            builder.UseMySQL(ConnectionString, b =>
            {
                //b.ExecutionStrategy();
            });
            return builder.Options;
        }
    }
}
