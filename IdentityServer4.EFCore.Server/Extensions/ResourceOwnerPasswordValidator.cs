using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.EFCore.Server.Data;
using IdentityServer4.EFCore.Server.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;

namespace IdentityServer4.EFCore.Server.Extensions
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly TestDbContext _dbContext;
        private readonly ISystemClock _clock;

        public ResourceOwnerPasswordValidator(TestDbContext dbContext, ISystemClock clock)
        {
            _dbContext = dbContext;
            _clock = clock;
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            if (user != null)
            {
                return user.Password.Equals(password);
            }

            return false;
        }

        public User FindByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (ValidateCredentials(context.UserName, context.Password))
            {
                var user = FindByUsername(context.UserName);

                context.Result = new GrantValidationResult(
                    user.Id.ToString(),
                    OidcConstants.AuthenticationMethods.Password,
                    _clock.UtcNow.UtcDateTime,
                    new HashSet<Claim>(new ClaimComparer()));
            }

            return Task.CompletedTask;
        }
    }
}
