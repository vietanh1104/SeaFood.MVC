using WebApp.Application.Models.ResponseModel;
using WebApp.Domain.Entities;
using WebApp.Shared.Helpers;

namespace WebApp.Application.Contracts.Infrastructure
{
    public interface IProductRepository
    {
        Task<Product> Add(Product product, CancellationToken cancellationToken = default);
        Task<Product> Update(Product product, CancellationToken cancellationToken = default);
        Task<int> Delete(List<Guid> ids, CancellationToken cancellationToken = default);
        Task<ProductDetailDto> GetById(Guid id);
        Task<PageResponse<Product>> Search(string productName, string? keyword, string? orderBy, bool isAsc,
            DateTime fromDate, DateTime toDate,
            int page, int pageSize);    
    }
}
