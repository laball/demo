using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity
{
    /// <summary>
    /// Summary description for User.
    /// 表名: emms_system_user
    /// 描述: 用户信息表
    /// </summary>
    [Table("emms_system_user")]
    public class User : EntityBase
    {
        /// <summary>
        /// Gets or sets 编码
        /// DB Type : varchar(20)
        /// Nullable: true
        /// </summary>
        [Column("Code")]
        [StringLength(20)]
        public virtual string Code { get; set; }

        /// <summary>
        /// Gets or sets 名称
        /// DB Type : varchar(100)
        /// Nullable: true
        /// </summary>
        [Column("Name")]
        [StringLength(100)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets 公司编码，外键；
        /// DB Type : varchar(20)
        /// Nullable: true
        /// </summary>
        [Column("CompanyCode")]
        [StringLength(20)]
        public virtual string CompanyCode { get; set; }

        /// <summary>
        /// Gets or sets 工作区编码，外键
        /// DB Type : varchar(20)
        /// Nullable: true
        /// </summary>
        [Column("WorkAreaCode")]
        [StringLength(20)]
        public virtual string WorkAreaCode { get; set; }

        /// <summary>
        /// Gets or sets 备件仓库编码，外键；
        /// DB Type : varchar(20)
        /// Nullable: true
        /// </summary>
        [Column("WareHouseCode")]
        [StringLength(20)]
        public virtual string WareHouseCode { get; set; }

        /// <summary>
        /// Gets or sets 电话号码
        /// DB Type : varchar(40)
        /// Nullable: true
        /// </summary>
        [Column("Phone")]
        [StringLength(40)]
        public virtual string Phone { get; set; }

        /// <summary>
        /// Gets or sets 密码
        /// DB Type : varchar(40)
        /// Nullable: true
        /// </summary>
        [Column("PassWord")]
        [StringLength(40)]
        public virtual string PassWord { get; set; }

        /// <summary>
        /// Gets or sets salt用于加密，创建用户时随机产生；
        /// DB Type : varchar(6)
        /// Nullable: true
        /// </summary>
        [Column("Salt")]
        [StringLength(6)]
        public virtual string Salt { get; set; }

        /// <summary>
        /// Gets or sets 需要重置密码；""：不需要；“X”：需要；
        /// DB Type : varchar(2)
        /// Nullable: true
        /// </summary>
        [Column("NeedResetPassword")]
        [StringLength(2)]
        public virtual string NeedResetPassword { get; set; }

        /// <summary>
        /// Gets or sets 角色ID，外键；
        /// DB Type : int
        /// Nullable: true
        /// </summary>
        [Column("RoleID")]
        public virtual int? RoleID { get; set; }

        /// <summary>
        /// Gets or sets 最后登录时间
        /// DB Type : datetime
        /// Nullable: true
        /// </summary>
        [Column("LastLoginTime")]
        public virtual DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// Gets or sets 最后操作时间
        /// DB Type : datetime
        /// Nullable: true
        /// </summary>
        [Column("LastOperateTime")]
        public virtual DateTime? LastOperateTime { get; set; }

        /// <summary>
        /// 是否启用，‘’：未启用，‘X’：启用；
        /// DB Type : varchar(2)
        /// Nullable: true
        /// </summary>
        [Column("Enabled")]
        [StringLength(2)]
        public virtual string Enabled { get; set; }
    }
}