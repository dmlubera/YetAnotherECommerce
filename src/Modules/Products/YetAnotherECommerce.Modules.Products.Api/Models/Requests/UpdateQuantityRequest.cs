using System;

namespace YetAnotherECommerce.Modules.Products.Api.Models.Requests
{
    public class UpdateQuantityRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}