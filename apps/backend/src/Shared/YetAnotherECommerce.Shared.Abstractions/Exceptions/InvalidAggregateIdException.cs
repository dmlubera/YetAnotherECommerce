namespace YetAnotherECommerce.Shared.Abstractions.Exceptions;

public class InvalidAggregateIdException() : YetAnotherECommerceException("Invalid aggregate id.")
{
    public override string ErrorCode => "invalid_aggregate_id";
}