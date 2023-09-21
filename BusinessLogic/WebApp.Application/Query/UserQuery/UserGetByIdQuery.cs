using AutoMapper;
using MediatR;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Dto.Responses;

namespace WebApp.Application.Query.UserQuery
{
    public class UserGetByIdQuery : IRequest<UserDto>
    {
        public Guid id { get; set; }
    }
    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserGetByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            return new UserDto();
        }
    }
}
