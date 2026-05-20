using YetAnotherECommerce.Modules.Catalog.Core.Entitites;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Catalog.Core.DomainEvents;

public record ProductCreated(Product Product) : IDomainEvent;