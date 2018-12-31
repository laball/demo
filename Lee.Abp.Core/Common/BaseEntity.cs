using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Lee.Abp.Core.Common
{
    public class BaseEntity : Entity<int>, IVersion, IMustHaveTenant
    {
        [Column(Order = 50)]
        public int TenantId { get; set; }

        /// <summary>
        /// 乐观锁字段；
        /// DatabaseGenerated必须添加，否则插入记录时MySql默认值：0000-00-00 00:00:00
        /// 与.net DateTime默认值（0001-01-01 0:00:00）不匹配导致更新失败；
        /// </summary>
        [Column(nameof(Version), TypeName = "timestamp", Order = 51), ConcurrencyCheck]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Version { get; set; }

        [Column("ID", Order = 0)]
        [Description("唯一标识")]
        public override int Id { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("CreateUser", Order = 52)]
        [MaxLength(20)]
        [Description("创建人")]
        public string CreateUser { get; set; } = "";

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Column("ModifyUser", Order = 53)]
        [MaxLength(20)]
        [Description("最后修改人")]
        public string ModifyUser { get; set; } = "";

        /// <summary>
        /// 新增时间
        /// </summary>
        [Column("CreateTime", Order = 54)]
        [Description("新增时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("ModifyTime", Order = 55)]
        [Description("修改时间")]
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 删除标识 
        /// </summary>
        [Column("DeleteFlag", Order = 56)]
        [MaxLength(1)]
        [Description("删除标识")]
        public virtual string DeleteFlag { get; set; } = "";
    }
}
