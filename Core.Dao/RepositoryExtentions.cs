using System;
using System.Data;
using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core.Dao
{
    /// <summary>
    /// Extension methods for IRepository.
    /// </summary>
    public static class RepositoryExtentions
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="repository">仓储</param>
        /// <returns>数据库连接</returns>
        public static IDbConnection GetDbConnection<T>(this IRepository<T> repository)
            where T : EntityBase
        {
            var efRepository = repository as IEFRepository<T>;
            if (efRepository != null)
            {
                // return efRepository.Context.Database;
                return null;
            }

            var nhRepository = repository as INHRepository<T>;
            if (nhRepository != null)
            {
                return nhRepository.Session.Connection;
            }

            throw new NotSupportedException("This Type of Repository is not supported.");
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="factoryMethod">工厂方法</param>
        /// <returns>数据库连接</returns>
        public static IDbConnection GetDbConnection<T>(this IRepository<T> repository, Func<IRepository<T>, IDbConnection> factoryMethod)
            where T : EntityBase
        {
            if (factoryMethod == null)
            {
                throw new ArgumentNullException(nameof(factoryMethod));
            }

            return factoryMethod.Invoke(repository);
        }
    }
}
