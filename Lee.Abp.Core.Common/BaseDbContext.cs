using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Abp.EntityFrameworkCore;
using Lee.Abp.Utils;
using Microsoft.EntityFrameworkCore;

namespace Lee.Abp.Core.Common
{
    public abstract class BaseDbContext : AbpDbContext
    {
        public BaseDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                // 1. Add the IsDeleted property
                entityType.GetOrAddProperty("DeleteFlag", typeof(string));

                // 2. Create the query filter

                var parameter = Expression.Parameter(entityType.ClrType);

                // EF.Property<bool>(post, "IsDeleted")
                var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(string));
                var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("DeleteFlag"));

                // EF.Property<bool>(post, "IsDeleted") == false
                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(""));

                // post => EF.Property<bool>(post, "IsDeleted") == false
                var lambda = Expression.Lambda(compareExpression, parameter);

                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[nameof(BaseEntity.CreateUser)] = HttpContext.Current.GetCurrentUserCode();
                        entry.CurrentValues[nameof(BaseEntity.CreateTime)] = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[nameof(BaseEntity.ModifyUser)] = HttpContext.Current.GetCurrentUserCode();
                        entry.CurrentValues[nameof(BaseEntity.ModifyTime)] = DateTime.Now;

                        break;
                }
            }
        }


    }
}
