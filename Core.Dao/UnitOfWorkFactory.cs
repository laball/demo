using System;
using Core.Entity;

namespace Core.Dao
{
    /// <summary>
    /// 工作单元工厂
    /// </summary>
    public static class UnitOfWorkFactory
    {
        /// <summary>
        /// 创建工作单元
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="repository">仓储</param>
        /// <returns>工作单元</returns>
        public static IUnitOfWork Create<T>(IRepository<T> repository)
            where T : EntityBase
        {
            // 面向抽象（接口或抽象类）编程
            var efRepository = repository as IEFRepository<T>;
            if (efRepository != null)
            {
                return new EFUnitOfWork(efRepository.Context);
            }

            var nhRepository = repository as INHRepository<T>;
            if (nhRepository != null)
            {
                return new NHUnitOfWork(nhRepository.Session);
            }

            throw new NotSupportedException("This Type of Repository is not supported.");
        }

        /// <summary>
        /// 创建工作单元 
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="factoryMethod">工厂方法</param>
        /// <returns>工作单元</returns>
        public static IUnitOfWork Create<T>(IRepository<T> repository, Func<IRepository<T>, IUnitOfWork> factoryMethod)
            where T : EntityBase
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (factoryMethod == null)
            {
                throw new ArgumentNullException(nameof(factoryMethod));
            }

            return factoryMethod.Invoke(repository);
        }
    }
}
