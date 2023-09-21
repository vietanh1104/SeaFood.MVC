using AutoMapper;
using WebApp.Application.Command.UserCommand;
using WebApp.Application.Dto.Responses;
using WebApp.Domain.Entities;

namespace WebApp.Application.Configuration.MapperProfile
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserAddCommand>().ReverseMap();
        }
    }
}
