using System;

namespace YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.Outbox;

public record OutboxMessage(Guid Id, DateTime OccurredOn, string Type, string Data, DateTime? ProcessedDate = null);