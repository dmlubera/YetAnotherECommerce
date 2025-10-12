using AutoMapper;
using System.Linq;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Modules.Orders.Core.Entities;

namespace YetAnotherECommerce.Modules.Orders.Core.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TotalPrice, opts => opts.MapFrom(src => src.OrderItems.Sum(x => x.UnitPrice * x.Quantity)));

            CreateMap<Order, OrderDetailsDto>()
                .ForMember(dest => dest.TotalPrice, opts => opts.MapFrom(src => src.OrderItems.Sum(x => x.UnitPrice * x.Quantity)));
        }
    }
}