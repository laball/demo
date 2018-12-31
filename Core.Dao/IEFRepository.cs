using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core.Dao
{
    public interface IEFRepository<T> : IRepository<T>
        where T : EntityBase
    {
        DbContext Context { get; }
    }
}
