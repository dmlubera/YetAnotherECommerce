using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Products.Core.DomainEvents
{
    public record PriceUpdated(Product Product, Price Price) : IDomainEvent;
}