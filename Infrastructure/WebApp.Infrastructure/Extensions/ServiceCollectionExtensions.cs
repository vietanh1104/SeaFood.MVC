using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Application.Command.UserCommand;
using WebApp.Application.Configuration.MapperProfile;
using WebApp.Application.Contracts;
using WebApp.Application.Contracts.Infrastructure;
using WebApp.Application.Query.ProductQuery;
using WebApp.Infrastructure.Repositories;

namespace WebApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // register automapper
            services.AddAutoMapper(typeof(UserMapperProfile));

            // di
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();


            // register mediatR
            services.AddMediatR(typeof(UserAddCommand));

            
            return services;

        }
    }
}
