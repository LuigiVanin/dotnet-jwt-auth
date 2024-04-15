using AutoMapper;
using UserJwt.Dtos.User;
using UserJwt.Helpers;
using UserJwt.Dtos.Auth;
using UserJwt.Models;
using UserJwt.Repositories;
using UserJwt.Services.Auth;

class AuthService(IUserRepository userRepository, IMapper mapper) : IAuthService
{
  private readonly IUserRepository _userRepository = userRepository;
  private readonly IMapper _mapper = mapper;

  public async Task<User> SignUp(SignUpDto signUpDto)
  {
    Console.WriteLine("AuthService.SignUp");
    if (signUpDto.Password != signUpDto.ConfirmPassword)
    {
      throw new AppException(StatusCodes.Status400BadRequest, "Passwords do not match");
    }

    var existentUser = await _userRepository.FindByEmail(signUpDto.Email);


    if (existentUser != null)
    {
      throw new AppException(StatusCodes.Status409Conflict, "User already exists");
    }

    var user = _mapper.Map<User>(signUpDto);
    user.Password = BCrypt.Net.BCrypt.HashPassword(signUpDto.Password);

    user = await _userRepository.Create(user);
    return user;
  }

  public async Task<SignInResponseDto> SignIn(SignInDto signInDto)
  {
    var existentUser = await _userRepository.FindByEmail(signInDto.Email);

    if (existentUser == null)
    {
      throw new AppException(StatusCodes.Status404NotFound, "User not found");
    }

    if (!BCrypt.Net.BCrypt.Verify(signInDto.Password, existentUser.Password))
    {
      throw new AppException(StatusCodes.Status401Unauthorized, "Invalid password");
    }

    var response = new SignInResponseDto
    {
      Token = "the jwt lies here",
      User = _mapper.Map<UserDto>(existentUser)
    };

    return response;
  }
}