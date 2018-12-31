using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lee.PostgreSQL.Entities
{
    [Table("GuidTable")]
    public class GuidTable
    {
        /// <summary>
        /// Guid => uuid
        /// 
        /// 需要create extension引入
        /// 否则报：42883: function uuid_generate_v4() does not exist
        /// see:http://www.cnblogs.com/cc-java/p/6904386.html
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        /// <summary>
        /// string without length => text
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }
    }
}
