namespace IPLocations.Api;

public interface IResult
{
    bool Success { get; }
}

public interface IResult<T> : IResult
{
    T Value { get; }
}

public class Result : IResult
{
    public bool Success { get; }

    protected Result(bool succeeded)
    {
        Success = succeeded;
    }

    public static Result Succeeded()
        => new(true);

    public static Result Failed()
        => new(false);
}

public class Result<T> : Result, IResult<T>
{
    private T _value;

    public T Value
    {
        get
        {
            if (!Success)
                throw new InvalidOperationException("Result did not succeed, result is invalid");
            return _value;
        }
    }

    public Result(bool succeeded, T value)
        : base(succeeded)
    {
        _value = value;
    }

    public static Result<T> Succeeded(T value)
        => new(true, value);

    public static new Result<T> Failed()
        => new(false, default);
}
