using System.Collections.Generic;
using System.Linq;

namespace YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object obj)
        => Equals(obj as ValueObject);

    private bool Equals(ValueObject other)
    {
        return other is not null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject left, ValueObject right)
        => left.Equals(right);

    public static bool operator !=(ValueObject left, ValueObject right)
        => !(left == right);

    public override int GetHashCode()
        => GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
}