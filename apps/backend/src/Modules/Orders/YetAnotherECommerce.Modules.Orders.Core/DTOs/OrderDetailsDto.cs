using System.Collections.Generic;
using YetAnotherECommerce.Modules.Orders.Core.Entities;

namespace YetAnotherECommerce.Modules.Orders.Core.DTOs
{
    public class OrderDetailsDto : OrderDto
    {
        public List<OrderItem> OrderItems { get; set; }
    }
}