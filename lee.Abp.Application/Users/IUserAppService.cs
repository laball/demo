using Abp.Application.Services;
using Lee.Abp.Application.Users.Dto;
using Lee.Abp.Common;
using System.Threading.Tasks;

namespace Lee.Abp.Application.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task Create(UserAppendInputDto input);
        Task Update(UserUpdateInputDto inputDto);
        Task<PagedResult<UserOutputDto>> GetList(UserQueryInputDto input);
    }
}
