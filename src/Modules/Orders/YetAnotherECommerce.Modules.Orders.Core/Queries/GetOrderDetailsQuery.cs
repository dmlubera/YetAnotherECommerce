using System;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries
{
    public class GetOrderDetailsQuery : IQuery<OrderDetailsDto>
    {
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }

        public GetOrderDetailsQuery(Guid customerId, Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}