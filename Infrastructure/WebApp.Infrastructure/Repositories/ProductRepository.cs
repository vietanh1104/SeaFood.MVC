using AutoMapper;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Models.ResponseModel;
using WebApp.Domain.Entities;
using WebApp.Shared.Helpers;
using Microsoft.Data.SqlClient;

namespace WebApp.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SeafoodContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(IMapper mapper, ILogger<ProductRepository> logger)
        {
            _context = new SeafoodContext();
            _mapper = mapper;
            _logger = logger;
        }
        
        public Task<Product> Add(Product product, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(List<Guid> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDetailDto> GetById(Guid id)
        {
            string connectionString = "Data Source=V002952\\MSSQLSERVER03;Initial Catalog=Seafood;Integrated Security=True";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string sql = @"SELECT p.Id, p.Description, c.Name AS CategoryName, p.ShopName as ShopeName, p.Name, p.ReviewProd, p.Price, p.PriceSale,
                                p.Amount, p.Note FROM Products p LEFT JOIN Categorys c ON c.Code = p.CategoryCode";
                var room = connection.Query<ProductDetailDto>(sql);
                var a = room.ToList();
            }
            return new ProductDetailDto();
        }

        public Task<PageResponse<Product>> Search(string productName, string? keyword, string? orderBy, bool isAsc, DateTime fromDate, DateTime toDate, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Update(Product product, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
