using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries
{
    public class BrowseCustomerOrdersQueryHandler : IQueryHandler<BrowseCustomerOrdersQuery, IList<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public BrowseCustomerOrdersQueryHandler(IOrderRepository orderRepository)
            => _orderRepository = orderRepository;

        public async Task<IList<Order>> HandleAsync(BrowseCustomerOrdersQuery query)
            => await _orderRepository.BrowseByCustomerAsync(query.CustomerId);
    }
}