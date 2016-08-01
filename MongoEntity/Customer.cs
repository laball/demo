using System.Collections.Generic;
using MongoDB.Bson;

namespace MongoEntity
{
    public class Customer : MongoEntityBase
    {
        public CustomerInfo Info { get; set; }
        public string Name { get; set; }
        public IList<Order> Orders { get; set; }
    }
}