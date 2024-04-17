using UserJwt.Dtos.User;

namespace UserJwt.src.Services.User
{
    public interface IUserService
    {
        Task<UserDto?> FindUserById(string id);
    }
}