using Abp.Dapper.Repositories;
using Abp.Domain.Repositories;
using Lee.Abp.Core.Common;

namespace Lee.Abp.Core.Users.Services
{
    public class UserManager : ManagerBase<User>, IUserManager
    {

        private readonly IDapperRepository<User> _userDapperRepository;

        public UserManager(IRepository<User> userRepo, IDapperRepository<User> userDapperRepository)
            : base(userRepo)
        {
            _userDapperRepository = userDapperRepository;
        }
    }
}

