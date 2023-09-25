using AutoMapper;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Models.ResponseModel;
using WebApp.Domain.Entities;
using WebApp.Shared.Helpers;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using WebApp.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

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
        
        public async Task<Product> Add(Product request, CancellationToken cancellationToken = default)
        {
            try
            {
                request.Id = Guid.NewGuid();
                await _context.Products.AddAsync(request);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogDebug($"Adding new product = {JsonConvert.SerializeObject(request)} successfully.");

                return request;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while adding new user = {JsonConvert.SerializeObject(request)}, " +
                    $"message = {ex.Message}");
                throw new ArgumentException($"Failed while adding new user = {JsonConvert.SerializeObject(request)}.");
            }
        }

        public Task<int> Delete(List<Guid> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDetailDto> GetById(Guid id)
        {
            string connectionString = "Server=V002952\\MSSQLSERVER03;Database=Seafood;User ID=sa;Password=Vti@1234;MultipleActiveResultSets=true;TrustServerCertificate=True";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT p.Id, p.Description, c.Name AS CategoryName, s.Name AS ShopName, p.Name, p.ReviewProd, p.Price, p.PriceSale," + 
                                $"p.Amount, p.Note FROM Products p LEFT JOIN Categorys c ON c.Code = p.CategoryCode " +
                                $" LEFT JOIN ShopSeafoods s ON s.Code = p.ShopCode WHERE p.Id = \'{id}\'";
                var room = await connection.QueryFirstOrDefaultAsync<ProductDetailDto>(sql);
                if(room is null)
                {
                    throw new ArgumentException($"Product with id = {id} not found.");
                }
                return room;
            }
        }

        public async Task<PageResponse<ProductDto>> Search(string? categoryCode, string? shopCode, string? keyword, 
            string? orderBy, bool isAsc, 
            DateTime fromDate, DateTime toDate, 
            int page = 1, int pageSize = 10)
        {
            var query = _context.Products.Where(p => !p.IsDeleted);

            if (!string.IsNullOrWhiteSpace(shopCode))
            {
                query = query.Where(p => p.ShopCode.ToLower().Contains(shopCode.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(categoryCode))
            {
                query = query.Where(p => p.CategoryCode.ToLower().Contains(categoryCode.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p => p.Name.ToLower().Contains(keyword.ToLower()));
            }

            if (fromDate != DateTime.MinValue)
            {
                var tempDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0, DateTimeKind.Local);
                query = query.Where(p => p.CreatedAt >= tempDate);
            }

            if (toDate != DateTime.MinValue)
            {
                var tempDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59, DateTimeKind.Local);
                query = query.Where(p => p.CreatedAt <= tempDate);
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                if (isAsc)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }


            var total = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize).Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PageResponse<ProductDto>
            {
                total = total,
                page = page,
                pageSize = data.Count,
                data = _mapper.Map<List<ProductDto>>(data)
            };
        }

    public Task<Product> Update(Product request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
