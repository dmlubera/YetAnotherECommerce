using System;

namespace YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.Inbox;

public record InboxMessage(Guid Id, DateTime OccurredOn, string Type, string Data, DateTime? ProcessedDate = null);