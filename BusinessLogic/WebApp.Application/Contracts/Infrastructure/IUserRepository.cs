using WebApp.Application.Dto.Responses;
using WebApp.Domain.Entities;
using WebApp.Shared.Helpers;

namespace WebApp.Application.Contracts.Infrastructure
{
    public interface IUserRepository
    {
        Task<User> Add(User request, CancellationToken cancellationToken = default);
        Task<int> Delete(List<Guid> ls, CancellationToken cancellationToken = default);
        Task<User> GetById(Guid id);
        Task<PageResponse<UserDto>> Search(string? categoryCode, string? shopCode,
            string? keyword, string? orderBy, bool isAsc, 
            DateTime fromDate, DateTime toDate, 
            int page, int pageSize);
        Task<User> Update(User request, CancellationToken cancellationToken = default);

        // account
        Task<User> Login(string username, string passwordHash, CancellationToken cancellationToken = default);

    }
}
