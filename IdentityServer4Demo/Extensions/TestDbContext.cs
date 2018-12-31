using System.Collections.Generic;

namespace IdentityServer4Demo
{
    public class TestDbContext
    {
        public List<User> Users { get; } = new List<User>
        {
             new User
             {
                 Id = 1,
                 Username = "bob",
                 Password = "password",
                 IsActive = true
             },
             new User
             {
                 Id = 2,
                 Username = "alice",
                 Password = "password",
                 IsActive = false
             }
        };
    }
}
