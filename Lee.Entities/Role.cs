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
    [Table("Demo_Role")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("Name")]
        [StringLength(20)]
        public string Code { get; set; }

        [Column("Type")]
        //[Range(1, 4)]//无效
        public int Type { get; set; }

        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }




        [NotMapped]
        public string NotMappedField { get; set; }



    }
}
