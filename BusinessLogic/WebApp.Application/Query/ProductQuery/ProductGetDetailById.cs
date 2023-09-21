using MediatR;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Models.ResponseModel;

namespace WebApp.Application.Query.ProductQuery
{
    public class ProductGetDetailById : IRequest<ProductDetailDto>
    {
        public Guid id { get; set; }
    }
    public class ProductGetDetailByIdHandler : IRequestHandler<ProductGetDetailById, ProductDetailDto>
    {
        private readonly IProductRepository _prodRepository;
        public ProductGetDetailByIdHandler(IProductRepository prodRepository)
        {
            _prodRepository = prodRepository;
        }
        public async Task<ProductDetailDto> Handle(ProductGetDetailById request, CancellationToken cancellationToken)
        {
            return await _prodRepository.GetById(request.id);
        }
    }
}
