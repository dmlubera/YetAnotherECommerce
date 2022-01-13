using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Orders.Core.DomainEvents
{
    public record OrderStatusChanged(Order Order, OrderStatus OrderStatus) : IDomainEvent;
}