using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class AddProductToCartCommand : ICommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        private AddProductToCartCommand() { }

        public AddProductToCartCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}