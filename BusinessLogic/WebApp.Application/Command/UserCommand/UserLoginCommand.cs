using AutoMapper;
using MediatR;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Dto.Responses;

namespace WebApp.Application.Command.UserCommand
{
    public class UserLoginCommand : IRequest<UserDto>
    {
        public string username { get; set; } = null!;
        public string passwordHash { get; set; } = null!;
    }
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserLoginCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Login(request.username, request.passwordHash, cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }
}
