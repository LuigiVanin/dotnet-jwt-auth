using Microsoft.EntityFrameworkCore;
using UserJwt.Context;
using UserJwt.Models;

namespace UserJwt.Repositories
{
    public class UserRepository(MySqlContext database) : IUserRepository
    {

        private readonly MySqlContext _database = database;

        public async Task<User?> FindByEmail(string email)
        {
            return await _database
                .Users
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> Create(User user)
        {
            _database.Users.Add(user);
            await _database.SaveChangesAsync();
            return user;
        }
    }
}