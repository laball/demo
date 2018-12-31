using System.Threading.Tasks;
using Lee.Abp.Application.Users;
using Lee.Abp.Application.Users.Dto;
using Shouldly;
using Xunit;

namespace Lee.Abp.Web.Test
{
    public class UserAppServiceTest : TestBase<IUserAppService>
    {
        [Fact]
        public async Task GetUsersTest()
        {
            var input = new UserQueryInputDto
            {
                Name = "1",
                PageNumber = 1,
                PageSize = 10
            };

            var result = await _service.GetList(input);
            result.Data.Count.ShouldBeGreaterThan(0);
        }
    }
}
