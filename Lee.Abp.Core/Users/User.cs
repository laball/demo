using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Lee.Abp.Core.Common;

namespace Lee.Abp.Core.Users
{
    [Table("Lee_User")]
    public class User : BaseEntity
    {
        [StringLength(10)]
        public string Code { get; set; }
        [StringLength(10)]
        public string Name { get; set; }

        public int Level { get; set; }

        [Column(Order = 20)]
        public int Test { get; set;  }

        [Column(Order = 0)]
        public int Test1 { get; set; }
    }
}
