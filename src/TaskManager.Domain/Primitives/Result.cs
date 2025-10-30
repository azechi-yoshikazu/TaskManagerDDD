namespace TaskManager.Domain.Primitives;

/// <summary>
/// Represents the result of an operation, which can be either a success or a failure.
/// </summary>
public sealed class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }

    private Result(bool isSuccess, Error? error)
    {
        if (isSuccess && error is not null)
            throw new InvalidOperationException("A successful result cannot have an error.");
        if (!isSuccess && error is null)
            throw new InvalidOperationException("A failure result must have an error.");
        IsSuccess = isSuccess;
        Error = error;
    }

    public static implicit operator Result(Error error) => Failure(error);

    // Non value result
    public static Result Success() => new Result(true, null);
    public static Result Failure(Error error) => new Result(false, error);

    // Value result
    public static Result<T> Success<T>(T value) => new Result<T>(true, value, null);
    public static Result<T> Failure<T>(Error error) => new Result<T>(false, default, error);
}

/// <summary>
/// Represents the result of an operation that returns a value, which can be either a success or a failure.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public Error? Error { get; }

    public Result(bool isSuccess, T? value, Error? error)
    {
        if (isSuccess && error is not null)
            throw new InvalidOperationException("A successful result cannot have an error.");
        if (!isSuccess && error is null)
            throw new InvalidOperationException("A failure result must have an error.");

        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static implicit operator Result<T>(T value) => Result.Success(value);
    public static implicit operator Result<T>(Error error) => Result.Failure<T>(error);
}