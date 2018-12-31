using Lee.Abp.Application.Common.Dto;

namespace Lee.Abp.Application.Users.Dto
{
    public class UserQueryInputDto : PagedQueryInputDtoBase
    {
        public string Name { get; set; }
    }
}
