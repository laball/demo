using System;
using System.Collections.Generic;

namespace CoreConsole
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public bool? Sex { get; set; }

        public virtual User IdNavigation { get; set; }
    }
}
