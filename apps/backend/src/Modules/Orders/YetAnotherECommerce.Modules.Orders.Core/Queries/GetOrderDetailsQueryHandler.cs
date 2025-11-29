using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries;

public class GetOrderDetailsQueryHandler(IOrderRepository orderRepository)
    : IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    public async Task<OrderDetailsDto> HandleAsync(GetOrderDetailsQuery query)
    {
        var order = await orderRepository.GetForCustomerByIdAsync(query.CustomerId, query.OrderId);
        if (order is null)
            throw new OrderDoesNotExistException(query.OrderId);

        return new OrderDetailsDto
        {
            Id = order.Id,
            Status = order.Status.ToString(),
            TotalPrice = order.TotalPrice,
            OrderItems = order.OrderItems.ToList()
        };
    }
}