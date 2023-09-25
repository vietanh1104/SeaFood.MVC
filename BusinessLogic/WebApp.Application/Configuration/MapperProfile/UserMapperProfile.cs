using AutoMapper;
using WebApp.Application.Command.UserCommand;
using WebApp.Application.Dto.Responses;
using WebApp.Domain.Entities;
using WebApp.Shared.Helpers;

namespace WebApp.Application.Configuration.MapperProfile
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, User>()
                .ForMember(des => des.Id, opt => opt.Ignore())
                .ForMember(des => des.CreatedAt, opt => opt.Ignore())
                .ForMember(des => des.CreatedBy, opt => opt.Ignore());
            CreateMap<User, UserDto>();
            CreateMap<PageResponse<User>, PageResponse<UserDto>>();
            CreateMap<User, UserAddCommand>().ReverseMap();
        }
    }
}
