using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Dto.Responses;
using WebApp.Domain.Entities;
using WebApp.Shared.Helpers;

namespace WebApp.Application.Command.UserCommand
{
    public class UserAddCommand : IRequest<UserDto>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string Username { get; set; } = null!;
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                   ErrorMessage = "The password must contain at least 8 characters, including at least one uppercase letter, one lowercase letter, one digit, and one special character (@, $, !, %, *, ?, or &)")]
        public string Password { get; set; } = null!;
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string RepeatPassword { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? Avarta { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Sex { get; set; } = 0;
        public string Mobile { get; set; } = null!;
        public string? Email { get; set; }
        public string? Company { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; } = "dev_local";
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
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
            request.Password = PasswordHelper.GetPasswordHash(request.Password); 
            var user = await _userRepository.Add(temp);
            return _mapper.Map<UserDto>(user);
        }
    }
}
