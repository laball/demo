using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace IdentityServer4Demo
{
    public class ProfileService : IProfileService
    {
        protected readonly TestDbContext _dbContext;
        protected readonly ILogger Logger;

        public ProfileService(TestDbContext dbContext, ILogger<ProfileService> logger)
        {
            _dbContext = dbContext;
            Logger = logger;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(Logger);

            if (context.RequestedClaimTypes.Any())
            {
                //var user = _dbContext.Users.FindBySubjectId(context.Subject.GetSubjectId());
                //if (user != null)
                //{
                //    context.AddRequestedClaims(user.Claims);
                //}
            }

            context.LogIssuedClaims(Logger);

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            Logger.LogDebug("IsActive called from: {caller}", context.Caller);

            var user = _dbContext.Users.FirstOrDefault(c => c.Id.ToString() == context.Subject.GetSubjectId());
            context.IsActive = user?.IsActive == true;

            return Task.CompletedTask;
        }
    }
}
