using WebApp.Domain.Entities;

namespace WebApp.Application.Contracts.Infrastructure
{
    public interface ICategoryRepository
    {
        Task<Category> Add(Category category, CancellationToken cancellationToken = default);
        Task<Category> Update(Category category, CancellationToken cancellationToken = default);
    }
}
