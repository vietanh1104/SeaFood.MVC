using AutoMapper;
using MediatR;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Dto.Responses;
using WebApp.Domain.Entities;

namespace WebApp.Application.Command.UserCommand
{
    public class UserEditProfileCommand : IRequest<UserDto>
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? Avarta { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Sex { get; set; }
        public string Mobile { get; set; } = null!;
        public string? Email { get; set; }
        public string? Company { get; set; }
    }
    public class UserEditProfileCommandHandler : IRequestHandler<UserEditProfileCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserEditProfileCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UserEditProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Update(_mapper.Map<User>(request), cancellationToken);
            return _mapper.Map<UserDto>(user); 
        }
    }
}
