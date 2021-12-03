using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries
{
    public class BrowseCustomerOrdersQueryHandler : IQueryHandler<BrowseCustomerOrdersQuery, IList<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public BrowseCustomerOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IList<OrderDto>> HandleAsync(BrowseCustomerOrdersQuery query)
            => _mapper.Map<List<OrderDto>>(await _orderRepository.BrowseByCustomerAsync(query.CustomerId));
    }
}