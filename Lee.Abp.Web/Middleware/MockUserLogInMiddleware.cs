using System.Security.Claims;
using System.Threading.Tasks;
using Lee.Abp.Core.Users;
using Microsoft.AspNetCore.Http;

namespace Lee.Abp.Web
{
    public class MockUserLogInMiddleware
    {
        private readonly RequestDelegate _next;

        public MockUserLogInMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("api"))
            {
                // mock user for test
                var user = new User
                {
                    Id = 1,
                    TenantId = 1,
                    Code = "dev_mock"
                };

                var claimIdentity = new ClaimsIdentity(ClaimsBuilder.Build(user), "abp_test");
                context.User.AddIdentity(claimIdentity);
            }

            await _next.Invoke(context);
        }
    }
}
