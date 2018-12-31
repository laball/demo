using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using Lee.Entities;
using Lee.Entities.Common;
using MySql.Data.Entity;

namespace Lee.EntityFramework
{

    //see https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework60.html
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class LeeDbContext : DbContext
    {
        public LeeDbContext()
                  : base("EMMS_DB")
        {
            Database.Log = s => Trace.WriteLine(s);

            //关闭懒加载（针对所有实体）；
            //Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Role>();
            modelBuilder.Entity<UserLoginLog>();
            modelBuilder.Entity<AllCommonFieldTable>();
            modelBuilder.Entity<GuidTable>();
            modelBuilder.Entity<OptimisticLockTable>().Property(c=>c.RowVersion).IsConcurrencyToken();
            
            //.Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MasterTable>();
            modelBuilder.Entity<ItemTable>();

            //.Property(c => c.RowVersion).IsConcurrencyToken();

            //最简化的，统一设置实体对应的表前缀（或后缀）方式；
            modelBuilder.Types().Configure(c => c.ToTable("Demo_" + c.ClrType.Name));
            //通过Convention统一设置实体对应的表前缀（或后缀）；
            //modelBuilder.Conventions.Add(new TableNameConvention());

            modelBuilder.Conventions.Add(new AttributeToColumnAnnotationConvention<DefaultValueAttribute, object>("DefaultValue", (p, attributes) => attributes.Single().DefaultValue));
            modelBuilder.Conventions.Add(new AttributeToColumnAnnotationConvention<DefaultValueAttribute, string>("DefaultValueSQL", (p, attributes) => attributes.Single().DefaultValueSql));

            base.OnModelCreating(modelBuilder);
        }
    }
}