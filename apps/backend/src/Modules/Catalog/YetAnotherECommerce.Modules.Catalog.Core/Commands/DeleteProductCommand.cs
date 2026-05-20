using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Catalog.Core.Commands;

public class DeleteProductCommand(Guid productId) : ICommand
{
    public Guid ProductId { get; set; } = productId;
}