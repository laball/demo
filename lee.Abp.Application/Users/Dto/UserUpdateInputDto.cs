using Abp.AutoMapper;
using Lee.Abp.Core.Users;

namespace Lee.Abp.Application.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class UserUpdateInputDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
