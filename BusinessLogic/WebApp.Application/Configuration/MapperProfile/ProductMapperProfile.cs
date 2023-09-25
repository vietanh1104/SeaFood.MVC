using AutoMapper;
using WebApp.Application.Models.ResponseModel;
using WebApp.Domain.Entities;
using WebApp.Shared.Helpers;

namespace WebApp.Application.Configuration.MapperProfile
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Product, ProductDto>();   
            CreateMap<Product, Product>()
                .ForMember(des => des.Id, opt => opt.Ignore())
                .ForMember(des => des.CreatedAt, opt => opt.Ignore())
                .ForMember(des => des.CreatedBy, opt => opt.Ignore())
                ;
            CreateMap<PageResponse<Product>, PageResponse<ProductDto>>();
        }
    }
}
