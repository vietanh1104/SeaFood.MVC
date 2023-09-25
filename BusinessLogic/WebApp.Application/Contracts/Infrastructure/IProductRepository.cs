using WebApp.Application.Models.ResponseModel;
using WebApp.Domain.Entities;
using WebApp.Shared.Helpers;

namespace WebApp.Application.Contracts.Infrastructure
{
    public interface IProductRepository
    {
        Task<Product> Add(Product request, CancellationToken cancellationToken = default);
        Task<Product> Update(Product request, CancellationToken cancellationToken = default);
        Task<int> Delete(List<Guid> ids, CancellationToken cancellationToken = default);
        Task<ProductDetailDto> GetById(Guid id);
        Task<PageResponse<ProductDto>> Search(string? categoryCode, string? shopCode, string? keyword, string? orderBy, bool isAsc,
            DateTime fromDate, DateTime toDate,
            int page = 1, int pageSize = 10);    
    }
}
