using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Entity;

namespace Core.Dao
{
    public interface IRepository<T>
        where T : EntityBase
    {
        T Get(int id);

        T Insert(T o);

        T Update(T o);

        void Delete(int id);

        void Delete(Expression<Func<T, bool>> predicate);

        void Delete(T entity);

        void LogicalDelete(IEnumerable<int> ids);

        T Single(Expression<Func<T, bool>> predicate);

        List<T> List();

        List<T> List(Expression<Func<T, bool>> exp);

        long Count();

        long Count(Expression<Func<T, bool>> predicate);

        IQueryable<T> Queryable();
    }
}