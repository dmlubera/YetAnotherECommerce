using System;

namespace YetAnotherECommerce.Modules.Orders.Core.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}