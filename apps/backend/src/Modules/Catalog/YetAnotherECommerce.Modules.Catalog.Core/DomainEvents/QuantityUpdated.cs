using YetAnotherECommerce.Modules.Catalog.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Catalog.Core.DomainEvents;

public record QuantityUpdated(Product Product, int Quantity) : IDomainEvent;