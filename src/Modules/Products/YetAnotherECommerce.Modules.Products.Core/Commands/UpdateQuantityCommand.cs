using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class UpdateQuantityCommand : ICommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public UpdateQuantityCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}