using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Dto.Responses;
using WebApp.Shared.Helpers;

namespace WebApp.Application.Command.UserCommand
{
    public class UserLoginCommand : IRequest<UserDto>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string username { get; set; } = null!;
        [Required(ErrorMessage = "{0} is required.")]
        public string password { get; set; } = null!;
        public bool rememberPassword { get; set; } = false;
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
            var user = await _userRepository.Login(request.username, PasswordHelper.GetPasswordHash(request.password), cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }
}
