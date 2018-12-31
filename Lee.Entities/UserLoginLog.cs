using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee.Entities
{
    [Table("Demo_UserLoginLog")]
    public class UserLoginLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [DefaultValue(1)]
        [Column("Type")]
        [Range(1, 2)]
        public int Type { get; set; }

        [Column("LoginTime")]
        public DateTime LoginTime { get; set; }

        [Column("IsDelete")]
        public bool IsDelete { get; set; }

    }
}
