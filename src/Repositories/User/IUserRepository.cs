using UserJwt.Models;

namespace UserJwt.Repositories
{
    public interface IUserRepository
    {
        Task<User?> FindByEmail(string email);
        Task<User?> FindById(string id);
        Task<User> Create(User user);
    }
}