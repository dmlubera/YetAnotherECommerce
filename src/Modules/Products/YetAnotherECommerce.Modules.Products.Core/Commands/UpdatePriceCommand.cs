using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public record UpdatePriceCommand(Guid ProductId, decimal Price) : ICommand;
}