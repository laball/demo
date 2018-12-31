using Core.Entity;
using NHibernate;

namespace Core.Dao
{
    /// <summary>
    /// NHibernate仓储接口
    /// </summary>
    /// <typeparam name="T">类型参数</typeparam>
    public interface INHRepository<T> : IRepository<T>
        where T : EntityBase
    {
        /// <summary>
        /// Gets NHibernate Session
        /// </summary>
        ISession Session { get; }
    }
}
