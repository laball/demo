using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoEntity
{
    public class Customer
    {
        public object _id { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public CustomerInfo Info { get; set; }
        public IList<Order> Orders { get; set; }
    }
}
