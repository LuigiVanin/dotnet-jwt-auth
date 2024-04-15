using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserJwt.Dtos.User;

namespace UserJwt.src.Services.User
{
    public interface IUserService
    {
        Task<UserDto?> FindUserById(string id);
    }
}