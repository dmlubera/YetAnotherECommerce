using System;

namespace YetAnotherECommerce.Shared.Abstractions.Results;

public record Result<TValue>
{
    protected Result(TValue value)
    {
        IsSucceeded = true;
        Value = value;
        Error = null;
    }

    protected Result(Error error)
    {
        IsSucceeded = false;
        Value = default;
        Error = error;
    }
    
    public bool IsSucceeded { get; }

    public TValue Value { get; }

    public Error Error { get; }

    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<Error, TResult> onError)
        => IsSucceeded ? onSuccess(Value) : onError(Error);
}

public record Result
{
    protected Result()
    {
        IsSucceeded = true;
        Error = null;
    }

    protected Result(Error error)
    {
        IsSucceeded = false;
        Error = error;
    }
    
    public bool IsSucceeded { get; }

    public Error Error { get; }

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<Error, TResult> onError)
        => IsSucceeded ? onSuccess() : onError(Error);
}

public record Error(string Code, string Message);