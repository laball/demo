// <copyright file="EFRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Core.Dao
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Core.Entity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// EF仓储实现
    /// </summary>
    /// <typeparam name="T">类型参数</typeparam>
    public class EFRepository<T> : IEFRepository<T>, IRepository<T>
        where T : EntityBase
    {
        public DbContext Context { get; }

        public EFRepository(DbContext context)
        {
            Context = context;
        }

        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public T Insert(T entity)
        {
            entity.CreateTime = DateTime.Now;

            return Context.Set<T>().Add(entity).Entity;
        }

        public T Update(T entity)
        {
            entity.ModifyTime = DateTime.Now;
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Delete(int id)
        {
            Delete(Get(id));
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
        }

        public void Delete(T entity)
        {
            Context.Set<T>()
                .Remove(entity);
        }

        public void LogicalDelete(IEnumerable<int> ids)
        {
            var items = Context.Set<T>().Where(c => ids.Contains(c.Id)).ToList();
            foreach (var item in items)
            {
                item.DeleteFlag = "X";
                item.ModifyTime = DateTime.Now;

                Update(item);
            }
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>()
                .Where(predicate)
                .FirstOrDefault();
        }

        public List<T> List()
        {
            return Context.Set<T>()
                .ToList();
        }

        public List<T> List(Expression<Func<T, bool>> exp)
        {
            return Context.Set<T>()
                .Where(exp)
                .ToList();
        }

        public long Count()
        {
            return Context.Set<T>()
                .Count();
        }

        public long Count(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>()
                .Where(predicate)
                .Count();
        }

        public IQueryable<T> Queryable()
        {
            return Context.Set<T>().AsQueryable();
        }
    }
}