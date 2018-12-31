using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Lee.Abp.Core.Common
{
    public class ManagerBase<TEntity> : IManagerBase<TEntity> where TEntity : class, IEntity<int>
    {
        private readonly IRepository<TEntity> _mainRepo;

        public ManagerBase(IRepository<TEntity> mainRepo)
        {
            _mainRepo = mainRepo;
        }

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await _mainRepo.LongCountAsync(predicate);
        }

        public virtual async Task Create(TEntity entity)
        {
            await _mainRepo.InsertAsync(entity);
        }

        public async  Task Update(TEntity entity)
        {
            await _mainRepo.UpdateAsync(entity);
        }

        public virtual async Task Delete(TEntity entity)
        {
            await _mainRepo.DeleteAsync(entity);
        }

        public virtual async Task Delete(int id)
        {
            await _mainRepo.DeleteAsync(id);
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _mainRepo.GetAsync(id);
        }

        public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return await _mainRepo.GetAllListAsync(predicate);
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return _mainRepo.GetAll();
        }

        public virtual async Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate)
        {
            return await _mainRepo.SingleAsync(predicate);
        }
    }
}
