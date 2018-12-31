namespace Lee.PostgreSQL.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Lee.PostgreSQL.EntityFramework.NpgsqlDbContext>
    {
        public Configuration()
        {
            CodeGenerator = new ExtendedMigrationCodeGenerator();
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Lee.PostgreSQL.EntityFramework.NpgsqlDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
