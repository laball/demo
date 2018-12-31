using Microsoft.EntityFrameworkCore;

namespace Core.Dao
{
    /// <summary>
    /// 工作单元，UOW模式实现
    /// 必须使用using或try finally
    /// see https://www.codeproject.com/Articles/526874/Repositorypluspattern-cplusdoneplusright
    /// <code>
    /// using(var uow = UnitOfWorkFactory.Create(repository))
    /// {
    ///     //do work...
    ///
    ///     uow.SaveChanges();
    /// }
    /// </code>
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public EFUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
        }

        public void SaveChanges() => _context.SaveChanges();
    }
}