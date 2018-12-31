using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Microsoft.AspNetCore.Http;

namespace Lee.Abp.Web
{
    public class AbpMiddleware
    {
        private readonly RequestDelegate _next;

        public AbpMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("api"))
            {
                var tenantid = context.Request.Headers[MultiTenancyConsts.TenantIdResolveKey];
                Trace.WriteLine("Tenantid: " + tenantid);

                var user = context.User;

                // TODO：伪造ABP Claim，用于识别用户和租户ID
                var chaims = new[]
                {
                new Claim(AbpClaimTypes.UserId,new System.Random().Next(1, 20).ToString()),
                new Claim(AbpClaimTypes.TenantId, tenantid)
            };

                var claimIdentity = new ClaimsIdentity(chaims, "abp");
                context.User.AddIdentity(claimIdentity);
            }         

            await _next.Invoke(context);
        }
    }
}
