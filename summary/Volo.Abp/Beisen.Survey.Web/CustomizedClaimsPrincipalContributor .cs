using System.Security.Claims;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;

namespace Beisen.Survey.Web
{
    public class CustomizedClaimsPrincipalContributor : IAbpClaimsPrincipalContributor, ITransientDependency
    {
        public async Task ContributeAsync(AbpClaimsPrincipalContributorContext context)
        {
            var identity = context.ClaimsPrincipal.Identities.FirstOrDefault();
            identity?.AddClaim(new Claim("UserId", "157"));

            await Task.CompletedTask;

            //var identity = context.ClaimsPrincipal.Identities.FirstOrDefault();
            //var userId = identity?.FindUserId();
            //if (userId.HasValue)
            //{
            //    var userService = context.ServiceProvider.GetRequiredService<IUserService>(); //Your custom service
            //    var socialSecurityNumber = await userService.GetSocialSecurityNumberAsync(userId.Value);
            //    if (socialSecurityNumber != null)
            //    {
            //        identity.AddClaim(new Claim("SocialSecurityNumber", socialSecurityNumber));
            //    }
            //}
        }
    }
}