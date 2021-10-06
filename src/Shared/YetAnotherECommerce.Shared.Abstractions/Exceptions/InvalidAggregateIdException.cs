namespace YetAnotherECommerce.Shared.Abstractions.Exceptions
{
    public class InvalidAggregateIdException : YetAnotherECommerceException
    {
        public override string ErrorCode => "invalid_aggregate_id";

        public InvalidAggregateIdException()
            : base("Invalid aggregate id.")
        {

        }
    }
}