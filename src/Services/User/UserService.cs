using AutoMapper;
using UserJwt.Dtos.User;
using UserJwt.Helpers;
using UserJwt.Repositories;

namespace UserJwt.src.Services.User
{
    public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
    {
        readonly IUserRepository _userRepository = userRepository;
        readonly IMapper _mapper = mapper;

        public async Task<UserDto?> FindUserById(string id)
        {
            var foundedUser = await _userRepository.FindById(id);

            if (foundedUser == null)
                throw new AppException(
                    StatusCodes.Status404NotFound,
                    "User not found"
                );


            var userResponse = _mapper.Map<UserDto>(foundedUser);

            return userResponse;
        }
    }
}