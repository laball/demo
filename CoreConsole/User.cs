using System;
using System.Collections.Generic;

namespace CoreConsole
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual UserDetail UserDetail { get; set; }
    }
}
