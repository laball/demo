using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Lee.Entities.Common;
using Lee.PostgreSQL.Entities;

namespace Lee.PostgreSQL.EntityFramework
{
    [DbConfigurationType(typeof(NpgsqlConfiguration))]
    public class NpgsqlDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<CommonFieldTable> CommonFieldTables { get; set; }

        public DbSet<GuidTable> GuidTables { get; set; }


        public NpgsqlDbContext()
            : base("PG_DB")
        {

        }

        public NpgsqlDbContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<NpgsqlDbContext>(null);
            //modelBuilder.HasDefaultSchema("public");
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("test");
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Properties().Configure(c =>
            {
                var name = c.ClrPropertyInfo.Name;
                var newName = name.ToLower();
                c.HasColumnName(newName);
            });

            modelBuilder.Conventions.Add(new AttributeToColumnAnnotationConvention<DefaultValueAttribute, object>("DefaultValue", (p, attributes) => attributes.Single().DefaultValue));
            modelBuilder.Conventions.Add(new AttributeToColumnAnnotationConvention<DefaultValueAttribute, string>("DefaultValueSQL", (p, attributes) => attributes.Single().DefaultValueSql));

            base.OnModelCreating(modelBuilder);
        }
    }
}
