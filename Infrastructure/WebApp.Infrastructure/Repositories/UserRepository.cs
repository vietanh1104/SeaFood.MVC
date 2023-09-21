using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
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

        public async Task<User> Add(User request, CancellationToken cancellationToken = default)
        {
            try
            {
                request.Id = Guid.NewGuid();
                await _context.Users.AddAsync(request);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogDebug($"Adding new user = {JsonConvert.SerializeObject(request)} successfully.");

                return request;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed while adding new user = {JsonConvert.SerializeObject(request)}, " +
                    $"message = {ex.Message}");
                throw new ArgumentException($"Failed while adding new user = {JsonConvert.SerializeObject(request)}.");
            }
        }

        public Task<int> Delete(List<Guid> ls, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _context.Users.Where(p => p.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (user is null)
            {
                throw new ArgumentException($"User not found.");
            }

            return user;
        }

        public async Task<User> Login(string username, string passwordHash, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _context.Users
                    .Where(p => (p.Username.Equals(username) || (!string.IsNullOrWhiteSpace(p.Email) && p.Email.Equals(username))) 
                    && p.PasswordHash.Equals(passwordHash))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
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
                throw new ArgumentException($"Failed while logging in with username = {username}");
            }
        }

        public Task<PageResponse<UserDto>> Search(string? categoryCode, string? shopCode, string? keyword, string? orderBy, bool isAsc, DateTime fromDate, DateTime toDate, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Update(User request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _context.Users.Where(p => p.Id == request.Id).FirstOrDefaultAsync();
                if (user is null)
                {
                    throw new ArgumentException($"User not found.");
                }

                _mapper.Map(request, user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while editting profile with request = {JsonConvert.SerializeObject(request)}" +
                    $"message = {ex.Message}");
                // fake
                throw new ArgumentException($"Failed while editting profile.");
            }
        }
    }
}