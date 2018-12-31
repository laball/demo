using System;
using Abp.AutoMapper;
using Core.Entity;

namespace Core.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserOutputDTO
    {
        /// <summary>
        /// 主键ID，自增
        /// DB Type : int
        /// Nullable: false
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 编码
        /// DB Type : varchar(20)
        /// Nullable: true
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// DB Type : varchar(100)
        /// Nullable: true
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 公司编码，外键；
        /// DB Type : varchar(20)
        /// Nullable: true
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 工作区编码，外键
        /// DB Type : varchar(20)
        /// Nullable: true
        /// </summary>
        public string WorkAreaCode { get; set; }

        /// <summary>
        /// 备件仓库编码，外键；
        /// DB Type : varchar(20)
        /// Nullable: true
        /// </summary>
        public string WareHouseCode { get; set; }

        /// <summary>
        /// 电话号码
        /// DB Type : varchar(40)
        /// Nullable: true
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 密码
        /// DB Type : varchar(40)
        /// Nullable: true
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// Salt用于加密，创建用户时随机产生；
        /// DB Type : varchar(6)
        /// Nullable: true
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 需要重置密码；""：不需要；“X”：需要；
        /// DB Type : varchar(2)
        /// Nullable: true
        /// </summary>
        public string NeedResetPassword { get; set; }

        /// <summary>
        /// 角色ID，外键；
        /// DB Type : int
        /// Nullable: true
        /// </summary>
        public int? RoleID { get; set; }

        /// <summary>
        /// 最后登录时间
        /// DB Type : datetime
        /// Nullable: true
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最后操作时间
        /// DB Type : datetime
        /// Nullable: true
        /// </summary>
        public DateTime? LastOperateTime { get; set; }

        /// <summary>
        /// 是否启用，‘’：未启用，‘X’：启用；
        /// DB Type : varchar(2)
        /// Nullable: true
        /// </summary>
        public string Enabled { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 新增时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 删除标识 string.empty：有效 "X"：已删除
        /// </summary>
        public string DeleteFlag { get; set; } = String.Empty;
    }
}