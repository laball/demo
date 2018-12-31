//-----------------------------------------------------------------------
// <copyright file="DemoDbContext.cs" company="Suning">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>17040404</author>
//-----------------------------------------------------------------------

using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core.Dao
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(p => p.DeleteFlag == "");

            // 全局过滤器
            // ***注意*** 此处不能用string.Empty，否则报错：Unable to cast object of type 'System.String' to type 'System.Linq.Expressions.LambdaExpression'.
            // 因此不建议使用字符串来做标志位，最好是使用数字，mysql中共布尔值对应的类型是：tinyint(1)
            base.OnModelCreating(modelBuilder);
        }
    }
}