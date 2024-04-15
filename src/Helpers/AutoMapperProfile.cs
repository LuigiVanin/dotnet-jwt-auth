using AutoMapper;
using UserJwt.Dtos.User;
using UserJwt.Dtos.Auth;
using UserJwt.Models;

namespace UserJwt.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SignUpDto, User>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }

    }
}