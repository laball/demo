using Core.Dao;
using Core.Dto;
using Core.Entity;
using Core.Extentions;
using Dapper;

namespace Core.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepo;

        public UserService(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public int CreateUser(CreateUserInputDTO userInputDTO)
        {
            using (var uow = UnitOfWorkFactory.Create(_userRepo))
            {
                var user = userInputDTO.Map<User>();
                _userRepo.Insert(user);

                uow.SaveChanges();

                //集成Dapper
                var count = _userRepo.GetDbConnection().ExecuteScalar<int>("select count(*) from emms_system_user");
                System.Diagnostics.Trace.WriteLine($"user count:{count}");

                return user.Id;
            }
        }

        public UserOutputDTO GetUserByID(int userID)
        {
            var user = _userRepo.Get(userID);
            return user.Map<UserOutputDTO>();       
        }
    }
}