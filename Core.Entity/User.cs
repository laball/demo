using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
