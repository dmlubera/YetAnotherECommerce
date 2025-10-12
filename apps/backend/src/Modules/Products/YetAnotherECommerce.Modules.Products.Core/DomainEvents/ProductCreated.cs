using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Products.Core.DomainEvents
{
    public record ProductCreated(Product product) : IDomainEvent;
}