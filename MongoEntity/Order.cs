using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoEntity
{
    public class Order
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime BuyTime { get; set; }
    }
}
