using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands;

public class CompleteOrderCommand(Guid orderId) : ICommand
{
    public Guid OrderId { get; set; } = orderId;
}