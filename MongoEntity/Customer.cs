using System;
using System.Collections.Generic;

namespace MongoEntity
{
    public class Customer : MongoEntityBase
    {
        public int Age { get; set; }
        public DateTime CreateOn { get; set; }
        public double Height { get; set; }
        public CustomerInfo Info { get; set; }
        public bool IsMail { get; set; }
        public string Name { get; set; }
        public IList<Order> Orders { get; set; }
        public float Weight { get; set; }
    }
}