using System.Linq;
using Abp;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.Extensions;
using Lee.Abp.Core;
using Lee.Abp.Core.Common;
using Lee.Abp.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace Lee.Abp.EntityFrameworkCore
{
    public class LeeAbpDbContext : AbpDbContext
    {

        /// <summary>
        /// 使用ABP必须使用DbSet，否则报错：
        /// 'Lee.Abp.Core.Users.Services.UserManager' is waiting for the following dependencies:
        /// - Service 'Abp.Domain.Repositories.IRepository`1[[Lee.Abp.Core.Users.User, Lee.Abp.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]' which was not registered.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TestTable> TestTables { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public LeeAbpDbContext(DbContextOptions<LeeAbpDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Role>();
            //.Property(c => c.Enabled)
            //.HasConversion<int>();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(bool))
                    {
                        property.SetValueConverter(new BoolToIntConverter());
                    }
                }
            }

            base.OnModelCreating(modelBuilder);

#if DEBUG
            //参考王如建写的，修改为支持.net core版本；
            var updater = new DbDescriptionUpdater<LeeAbpDbContext>(this);
            updater.UpdateDatabaseDescriptions();
#endif
        }
    }
}
