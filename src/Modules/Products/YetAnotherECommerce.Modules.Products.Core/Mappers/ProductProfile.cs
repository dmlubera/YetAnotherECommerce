using AutoMapper;
using YetAnotherECommerce.Modules.Products.Core.DTOs;
using YetAnotherECommerce.Modules.Products.Core.Entitites;

namespace YetAnotherECommerce.Modules.Products.Core.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductDetailsDto>();
        }
    }
}