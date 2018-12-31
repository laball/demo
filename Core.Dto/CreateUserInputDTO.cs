using Abp.AutoMapper;
using Core.Entity;

namespace Core.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(User))]
    public class CreateUserInputDTO
    {
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
    }
}