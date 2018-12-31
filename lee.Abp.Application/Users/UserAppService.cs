using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Events.Bus;
using Lee.Abp.Application.Extensions;
using Lee.Abp.Application.Handlers;
using Lee.Abp.Application.Users.Dto;
using Lee.Abp.Common;
using Lee.Abp.Core.Users;
using Lee.Abp.Core.Users.Services;
using Microsoft.EntityFrameworkCore;

namespace Lee.Abp.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserManager _userManager;

        public IEventBus EventBus { get; set; }

        public UserAppService(IUserManager userManager)
        {
            _userManager = userManager;
            EventBus = NullEventBus.Instance;
        }

        public async Task Create(UserAppendInputDto input)
        {
            var user = input.MapTo<User>();
            await _userManager.Create(user);
            //await EventBus.TriggerAsync(new CreateUserEvent { Name = input.Name, Code = input.Code });
        }

        public async Task Update(UserUpdateInputDto inputDto)
        {
            var user = await _userManager.GetById(inputDto.Id);
            if (user == null)
            {
                throw new APIException("用户信息不存在。");
            }

            inputDto.MapTo(user);
            await _userManager.Update(user);
        }
        public Task<PagedResult<UserOutputDto>> GetList(UserQueryInputDto input)
        {
            return _userManager.Queryable()
                .AsNoTracking()
                .Where(c => EF.Functions.Like(c.Name, $"%{input.Name}%"))
                .Paged<User, UserOutputDto>(input);
        }
    }
}
