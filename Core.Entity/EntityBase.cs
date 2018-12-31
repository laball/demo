// <copyright file="EntityBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets 主键ID，自增
        /// DB Type : int
        /// Nullable: false
        /// </summary>
        [Column("ID")]
        [Key]
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets 最后修改人
        /// </summary>
        [Column("CreateUser")]
        [StringLength(40)]
        public virtual string CreateUser { get; set; }

        /// <summary>
        /// Gets or sets 新增时间
        /// </summary>
        [Column("CreateTime")]
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// Gets or sets 最后修改人
        /// </summary>
        [Column("ModifyUser")]
        [StringLength(40)]
        public virtual string ModifyUser { get; set; }

        /// <summary>
        /// Gets or sets 修改时间
        /// </summary>
        [Column("ModifyTime")]
        public virtual DateTime? ModifyTime { get; set; }

        /// <summary>
        /// Gets or sets 删除标识 string.empty：有效 "X"：已删除
        /// </summary>
        [Column("DeleteFlag")]
        [StringLength(2)]
        public virtual string DeleteFlag { get; set; } = string.Empty;
    }
}