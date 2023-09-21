using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Dto.Responses;
using WebApp.Domain.Entities;
using WebApp.Shared.Helpers;

namespace WebApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SeafoodContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository( IMapper mapper, ILogger<UserRepository> logger)
        {
            _context = new SeafoodContext();
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<User> Add(User user, CancellationToken cancellationToken = default)
        {
            try
            {
                user.Id = Guid.NewGuid();
                user.CreatedBy = !string.IsNullOrWhiteSpace(user.CreatedBy) ? user.CreatedBy : "dev_local";
                user.CreatedAt = DateTime.UtcNow;
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogDebug($"Adding new user = {JsonConvert.SerializeObject(user)} successfully.");

                return user;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed while adding new user = {JsonConvert.SerializeObject(user)}, " +
                    $"message = {ex.Message}");
                throw new ArgumentException($"Failed while adding new user = {JsonConvert.SerializeObject(user)}, " +
                    $"message = {ex.Message}");
            }
        }

        public Task<int> Delete(List<Guid> ls, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetById(List<Guid> ls, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Login(string username, string passwordHash, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(p =>
                    (p.Username.Equals(username) || (!string.IsNullOrWhiteSpace(p.Email) && p.Email.Equals(username))) 
                    && p.PasswordHash.Equals(passwordHash));
                if(user is null)
                {
                    throw new ArgumentException($"Password or Username is incorrect.");
                }
                return user;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed while logging in with username = {username}, passwordHash = {passwordHash} " +
                    $"message = {ex.Message}");
                // fake
                return new User();
                throw new ArgumentException($"Failed while logging in with username = {username}" +
                    $"message = {ex.Message}");
            }
        }

        public Task<PageResponse<UserDto>> Search(Guid roomId, string keyword, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}