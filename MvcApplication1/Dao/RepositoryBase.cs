using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Dao
{
    public class Repository<T> :IRepository<T> where T :class
    {
        public T Get(object id)
        {
            throw new NotImplementedException();
        }

        public object Save(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}