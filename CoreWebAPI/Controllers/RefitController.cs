using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CoreWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Refit")]
    public class RefitController : BaseController
    {
        [Route("User")]
        public HttpResponse<User> GetUser()
        {
            var user = new User
            {
                ID = 125,
                Name = "Lee"
            };

            return Success(user);
        }

        [Route("Users")]
        public HttpResponse<IEnumerable<User>> GetUsers()
        {
            var users = new[] {
                new User
            {
                ID = 125,
                Name = "Lee"
            },
                new User
            {
                ID = 128,
                Name = "Leo"
            }
            };

            return Success(users.AsEnumerable());
        }
    }
}