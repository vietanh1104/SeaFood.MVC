using MediatR;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Models.ResponseModel;
using WebApp.Shared.Helpers;

namespace WebApp.Application.Query.ProductQuery
{
    public class ProductSearchQuery : IRequest<PageResponse<ProductDto>>
    {
        public string? categoryCode { get; set; }
        public string? shopCode { get; set; }
        public string? keyword { get; set; }
        public string? orderBy { get; set; }
        public bool isAsc { get; set; } = true;
        public DateTime fromDate { get; set; } = DateTime.MinValue;
        public DateTime toDate { get; set; } = DateTime.MinValue;
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
    public class ProductSearchQueryHandler : IRequestHandler<ProductSearchQuery, PageResponse<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        public async Task<PageResponse<ProductDto>> Handle(ProductSearchQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.Search(request.categoryCode, request.shopCode, request.keyword,
                request.orderBy, request.isAsc, request.fromDate, request.toDate, request.pageSize, request.pageSize);
        }
    }
}
