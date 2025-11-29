using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries;

public class BrowseQueryHandler(IOrderRepository orderRepository) : IQueryHandler<BrowseQuery, IReadOnlyList<OrderDto>>
{
    public async Task<IReadOnlyList<OrderDto>> HandleAsync(BrowseQuery query)
    {
        var orders = await orderRepository.BrowseAsync();

        return orders.Select(x => new OrderDto
        {
            Id = x.Id,
            Status = x.Status.ToString(),
            TotalPrice = x.TotalPrice
        }).ToList().AsReadOnly();
    }
}