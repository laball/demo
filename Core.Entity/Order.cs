using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime CreateOn { get; set; }
        public User User { get; set; }
    }
}
