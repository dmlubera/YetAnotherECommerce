using YetAnotherECommerce.Modules.Catalog.Core.Entities;
using YetAnotherECommerce.Modules.Catalog.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Catalog.Core.DomainEvents;

public record PriceUpdated(Product Product, Price Price) : IDomainEvent;