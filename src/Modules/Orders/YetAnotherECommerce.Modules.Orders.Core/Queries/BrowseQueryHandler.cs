using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries
{
    public class BrowseQueryHandler : IQueryHandler<BrowseQuery, IList<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public BrowseQueryHandler(IOrderRepository orderRepository)
            => _orderRepository = orderRepository;

        public Task<IList<Order>> HandleAsync(BrowseQuery query)
            => _orderRepository.BrowseAsync();
    }
}