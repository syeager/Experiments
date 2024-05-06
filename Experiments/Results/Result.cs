using System.Diagnostics.CodeAnalysis;

namespace Results;

public readonly struct Result
{
    private static readonly Result OkResult = new(null);

    public string? ErrorMessage { get; }
    [MemberNotNullWhen(false, nameof(ErrorMessage))]
    public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorMessage);

    private Result(string? errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public static Result Ok() => OkResult;
    public static Result Error(string message) => new(message);

    public void OnOk(Action action)
    {
        if (IsSuccess)
        {
            action();
        }
    }

    public Result OnError(Action<string> action)
    {
        if (IsSuccess is false)
        {
            action(ErrorMessage);
        }

        return this;
    }
}

public readonly struct Result<T>
    where T : class
{
    private readonly T? value;

    public string? ErrorMessage { get; }
    [MemberNotNullWhen(true, nameof(value))]
    [MemberNotNullWhen(false, nameof(ErrorMessage))]
    public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorMessage) && value is not null;

    private Result(string? errorMessage, T? value)
    {
        ErrorMessage = errorMessage;
        this.value = value;
    }

    public static Result<T> Ok(T value) => new(null, value);
    public static Result<T> Error(string message) => new(message, null);

    public T Extract()
    {
        if (IsSuccess is false)
        {
            throw new AccessingErrorResultValueException(ErrorMessage);
        }

        return value;
    }

    public void OnOk(Action<T> action)
    {
        if (IsSuccess)
        {
            action(value);
        }
    }

    public Result<T> OnError(Action<string> action)
    {
        if (IsSuccess is false)
        {
            action(ErrorMessage);
        }

        return this;
    }
}
