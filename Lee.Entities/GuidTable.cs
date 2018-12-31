using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lee.Entities
{
    [Table("Demo_GuidTable")]
    public class GuidTable
    {
        /// <summary>
        /// Guid => char(36)
        /// 
        /// 由于Mysql不支持字段默认值使用函数，因此会创建触发器处理默认值；
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        /// <summary>
        /// string without length => longtext
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }
    }
}
