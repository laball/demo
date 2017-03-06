using System;
using System.Collections.Generic;

namespace CoreConsole
{
    public partial class Order
    {
        public int Id { get; set; }
        public double? Sum { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
