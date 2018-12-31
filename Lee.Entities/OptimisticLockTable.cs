using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lee.Entities
{
    /// <summary>
    /// 乐观锁测试表
    /// </summary>
    [Table("Demo_OptimisticLockTable")]
    public class OptimisticLockTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[ConcurrencyCheck]
        [Column("RowVersion", TypeName = "timestamp")]
        public DateTime RowVersion { get; set; }

        //[Column("RowVersion", TypeName = "timestamp")]
        //public byte[] RowVersion { get; set; }//mysql中不能这样映射；只能映射到DateTime；

        [Column("Name")]
        [StringLength(10)]
        public string Name { get; set; }
    }
}
