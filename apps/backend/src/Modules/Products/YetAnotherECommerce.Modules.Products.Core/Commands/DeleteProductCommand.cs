using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands;

public class DeleteProductCommand(Guid productId) : ICommand
{
    public Guid ProductId { get; set; } = productId;
}