using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Lee.Abp.Core;
using Lee.Abp.Core.Common;
using Lee.Abp.Core.Users;

namespace Lee.Abp.Core
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [Description("用户角色信息表")]
    [Table("wcs_system_user_role")]
    public class UserRole : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Description("用户ID")]
        public int UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Description("角色ID")]
        public int RoleId { get; set; }
    }
}