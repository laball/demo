using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Lee.Abp.Core.Common;

namespace Lee.Abp.Core
{
    [Table("Lee_Role")]
    public class Role : BaseEntity
    {
        public bool Enabled { get; set; }
    }
}
