using AutoMapper;
using YetAnotherECommerce.Modules.Products.Core.DTOs;
using YetAnotherECommerce.Modules.Products.Core.Entitites;

namespace YetAnotherECommerce.Modules.Products.Core.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price.Value))
                .ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.Quantity.Value));

            CreateMap<Product, ProductDetailsDto>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price.Value))
                .ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.Quantity.Value));
        }
    }
}