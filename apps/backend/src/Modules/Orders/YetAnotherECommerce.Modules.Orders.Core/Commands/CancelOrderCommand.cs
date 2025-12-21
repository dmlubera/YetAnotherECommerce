using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands;

public class CancelOrderCommand(Guid customerId, Guid orderId) : ICommand
{
    public Guid CustomerId { get; set; } = customerId;
    public Guid OrderId { get; set; } = orderId;
}