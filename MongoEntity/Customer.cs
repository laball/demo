using System.Collections.Generic;
using MongoDB.Bson;

namespace MongoEntity
{
    public class Customer : MongoEntityBase
    {
        public string Name { get; set; }
        public CustomerInfo Info { get; set; }
        public IList<Order> Orders { get; set; }
    }
}
