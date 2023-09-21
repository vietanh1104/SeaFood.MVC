using WebApp.Application.Dto.Responses;
using WebApp.Domain.Entities;
using WebApp.Shared.Helpers;

namespace WebApp.Application.Contracts.Infrastructure
{
    public interface IUserRepository
    {
        Task<User> Add(User user, CancellationToken cancellationToken = default);
        Task<int> Delete(List<Guid> ls, CancellationToken cancellationToken = default);
        Task<UserDto> GetById(List<Guid> ls, CancellationToken cancellationToken = default);
        Task<PageResponse<UserDto>> Search(Guid roomId, string keyword, int page, int pageSize);
        Task<User> Update(User user, CancellationToken cancellationToken = default);

        // account
        Task<User> Login(string username, string passwordHash, CancellationToken cancellationToken = default);

    }
}
