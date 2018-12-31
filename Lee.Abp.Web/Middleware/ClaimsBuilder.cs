using System;
using System.Collections.Generic;
using System.Security.Claims;
using Abp.Runtime.Security;
using Lee.Abp.Core.Users;

namespace Lee.Abp.Web
{
    public static class ClaimsBuilder
    {
        public static IEnumerable<Claim> Build(User user)
        {
            if (user == null)
            {
                throw new NullReferenceException(nameof(user));
            }

            var claims = new HashSet<Claim>();
            claims.Add(new Claim(AbpClaimTypes.UserId, user.Id.ToString()));

            //if (user.TenantId.HasValue)
            //{
            claims.Add(new Claim(AbpClaimTypes.TenantId, user.TenantId.ToString()));
            //}

            claims.Add(new Claim("UserCode", user.Code));
            claims.Add(new Claim("UserName", user.Code));

            return claims;
        }
    }
}
