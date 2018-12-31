using System;

namespace Core.Dao
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 保存修改
        /// </summary>
        void SaveChanges();
    }
}