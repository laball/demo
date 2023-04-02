using Beisen.Survey.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Beisen.Survey.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
    public class SvrveyDbContextFactory : IDesignTimeDbContextFactory<SurveyDbContext>
    {
        public SurveyDbContext CreateDbContext(string[] args)
        {
            //BookStoreEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<SurveyDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new SurveyDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Beisen.Survey.Web/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}