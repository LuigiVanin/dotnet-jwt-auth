using AutoMapper;
using UserJwt.Dtos.User;
using UserJwt.Helpers;
using UserJwt.Dtos.Auth;
using UserJwt.Models;
using UserJwt.Repositories;
using UserJwt.Services.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


class AuthService(
  IUserRepository userRepository,
  IMapper mapper,
  IConfiguration config
) : IAuthService
{
  private readonly IUserRepository _userRepository = userRepository;
  private readonly IMapper _mapper = mapper;
  private readonly IConfiguration _config = config;

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

    var claims = new[] {
      new Claim("user_id", existentUser.Id),
      new Claim("email", existentUser.Email),
    };

    var secretKey = _config.GetValue<string>("JwtSecretKey")
      ?? throw new AppException(StatusCodes.Status500InternalServerError, "Internal server error");

    var jwt = new JwtSecurityToken(
      claims: claims,
      expires: DateTime.UtcNow.AddHours(24),
      signingCredentials: new SigningCredentials(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        SecurityAlgorithms.HmacSha256
      )
    );

    var jwtString = new JwtSecurityTokenHandler().WriteToken(jwt);

    var response = new SignInResponseDto
    {
      Token = jwtString,
      User = _mapper.Map<UserDto>(existentUser)
    };

    return response;
  }
}