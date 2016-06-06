using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class Customer : Entity
    {
        public virtual string AccountId { get; set; }
        public virtual string Address { get; set; }
    }
}
