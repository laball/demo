using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Lee.Abp.Core.Common
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure<TDbContext>(
             this DbContextOptionsBuilder<TDbContext> dbContextOptions,
            string connectionString
        ) where TDbContext : DbContext
        {
            dbContextOptions.UseMySQL(connectionString);
        }

        public static void Configure<TDbContext>(
            this DbContextOptionsBuilder<TDbContext> dbContextOptions,
            DbConnection connection
        ) where TDbContext : DbContext
        {
            dbContextOptions.UseMySQL(connection);
        }
    }
}
