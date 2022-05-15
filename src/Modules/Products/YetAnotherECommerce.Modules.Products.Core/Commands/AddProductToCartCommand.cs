using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class AddProductToCartCommand : ICommand
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        private AddProductToCartCommand() { }

        public AddProductToCartCommand(Guid customerId, Guid productId, int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}