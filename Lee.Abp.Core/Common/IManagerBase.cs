using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Services;

namespace Lee.Abp.Core.Common
{
    public interface IManagerBase<TEntity> : IDomainService where TEntity : class, IEntity<int>
    {
        Task<TEntity> GetById(int id);
        Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate);
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(int id);
        Task<long> Count(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Queryable();
    }
}
