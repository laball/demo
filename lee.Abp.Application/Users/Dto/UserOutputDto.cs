using Abp.AutoMapper;
using Lee.Abp.Core.Users;

namespace Lee.Abp.Application.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserOutputDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
