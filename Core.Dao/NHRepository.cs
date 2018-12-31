using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Entity;
using NHibernate;

namespace Core.Dao
{
    /// <summary>
    /// NHibernate仓库实现
    /// </summary>
    /// <typeparam name="T">泛型参数</typeparam>
    public class NHRepository<T> : INHRepository<T>, IRepository<T>
        where T : EntityBase
    {
        public ISession Session { get; }

        public NHRepository(ISession session)
        {
            Session = session;
        }

        public long Count()
        {
            return Session.Query<T>().LongCount();
        }

        public long Count(Expression<Func<T, bool>> predicate)
        {
            return Session.Query<T>().Where(predicate).LongCount();
        }

        public void Delete(int id)
        {
            var entity = Session.Get<T>(id);
            Session.Delete(entity);
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var entities = Session.Query<T>().Where(predicate).ToList();
            foreach (var entity in entities)
            {
                Session.Delete(entity);
            }
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
        }

        public T Get(int id)
        {
            return Session.Get<T>(id);
        }

        public T Insert(T entity)
        {
            Session.Save(entity);
            return entity;
        }

        public List<T> List()
        {
            return Session.Query<T>().ToList();
        }

        public List<T> List(Expression<Func<T, bool>> exp)
        {
            return Session.Query<T>().Where(exp).ToList();
        }

        public void LogicalDelete(IEnumerable<int> ids)
        {
            var entities = Session.Query<T>().Where(c => ids.Contains(c.Id)).ToList();
            foreach (var entity in entities)
            {
                entity.DeleteFlag = "X";
                Session.Update(entity);
            }
        }

        public IQueryable<T> Queryable()
        {
            return Session.Query<T>();
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return Session.Query<T>()
                .Where(predicate)
                .Take(1)
                .FirstOrDefault();
        }

        public T Update(T entity)
        {
            Session.Update(entity);
            return entity;
        }
    }
}
