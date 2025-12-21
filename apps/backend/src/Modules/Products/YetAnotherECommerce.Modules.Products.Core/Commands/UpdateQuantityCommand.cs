using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands;

public class UpdateQuantityCommand(Guid productId, int quantity) : ICommand
{
    public Guid ProductId { get; set; } = productId;
    public int Quantity { get; set; } = quantity;
}