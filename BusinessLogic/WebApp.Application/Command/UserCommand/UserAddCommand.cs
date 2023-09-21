using AutoMapper;
using MediatR;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Dto.Responses;
using WebApp.Domain.Entities;

namespace WebApp.Application.Command.UserCommand
{
    public class UserAddCommand : IRequest<UserDto>
    {
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? Avarta { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Sex { get; set; } = 0;
        public string Mobile { get; set; } = null!;
        public string? Email { get; set; }
        public string? Company { get; set; }
        public string? CreatedBy { get; set; } = "dev_local";
    }
    public class UserAddCommandHandler : IRequestHandler<UserAddCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserAddCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(UserAddCommand request, CancellationToken cancellationToken)
        {
            var temp = _mapper.Map<User>(request);
            var user = await _userRepository.Add(temp);
            return _mapper.Map<UserDto>(user);
        }
    }
}
