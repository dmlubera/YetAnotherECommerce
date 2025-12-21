using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands;

public record AddProductToCartCommand(Guid CustomerId, Guid ProductId, int Quantity) : ICommand;