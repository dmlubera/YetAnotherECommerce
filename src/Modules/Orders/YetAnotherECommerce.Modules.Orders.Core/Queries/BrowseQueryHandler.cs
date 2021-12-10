using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries
{
    public class BrowseQueryHandler : IQueryHandler<BrowseQuery, IReadOnlyList<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public BrowseQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<OrderDto>> HandleAsync(BrowseQuery query)
            => _mapper.Map<IReadOnlyList<OrderDto>>(await _orderRepository.BrowseAsync());
    }
}