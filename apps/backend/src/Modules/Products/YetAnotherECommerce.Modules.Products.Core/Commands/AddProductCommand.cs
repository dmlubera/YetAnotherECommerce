using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands;

public record AddProductCommand(string Name, string Description, decimal Price, int Quantity)
    : ICommand;