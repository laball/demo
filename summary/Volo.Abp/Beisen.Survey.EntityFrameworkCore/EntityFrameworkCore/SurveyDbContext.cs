using Beisen.Survey.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Beisen.Survey.EntityFrameworkCore.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class SurveyDbContext : AbpDbContext<SurveyDbContext>
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SurveyTask>(c =>
            {
                c.ToTable(SurveyConsts.DbTablePrefix + "SurveyTask", SurveyConsts.DbSchema);
                c.ConfigureByConvention();
                c.Property(c => c.SurveyName).IsRequired().HasMaxLength(120);
            });
        }
    }
}