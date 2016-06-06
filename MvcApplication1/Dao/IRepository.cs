using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Dao
{
    public interface IRepository<T> where T :class
    {
        T Get(object id);

        object Save(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}