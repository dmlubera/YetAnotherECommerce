using System;

namespace YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

public interface IAuditable
{
    public DateTime? CreatedAt { get; }
    public DateTime? LastUpdatedAt { get; }
}