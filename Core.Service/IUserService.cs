using Core.Dto;

namespace Core.Service
{
    public interface IUserService
    {
        int CreateUser(CreateUserInputDTO userInputDTO);

        UserOutputDTO GetUserByID(int userID);
    }
}