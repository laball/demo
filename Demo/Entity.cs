using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public abstract class Entity
    {
        public virtual bool IsDelete
        {
            get;
            set;
        }
    }
}
