using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries;

public class BrowseCustomerOrdersQueryHandler(IOrderRepository orderRepository)
    : IQueryHandler<BrowseCustomerOrdersQuery, IReadOnlyList<OrderDto>>
{
    public async Task<IReadOnlyList<OrderDto>> HandleAsync(BrowseCustomerOrdersQuery query)
    {
        var orders = await orderRepository.BrowseByCustomerAsync(query.CustomerId);

        return orders.Select(x => new OrderDto
        {
            Id = x.Id,
            Status = x.Status.ToString(),
            TotalPrice = x.TotalPrice
        }).ToList().AsReadOnly();
    }
}