using Abp.AutoMapper;
using Lee.Abp.Core.Users;

namespace Lee.Abp.Application.Users.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(User))]
    public class UserAppendInputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}
