using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class DeleteProductCommand : ICommand
    {
        public Guid ProductId { get; set; }

        public DeleteProductCommand(Guid productId)
        {
            ProductId = productId;
        }
    }
}