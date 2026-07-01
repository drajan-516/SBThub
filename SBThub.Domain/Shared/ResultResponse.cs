using SBThub.Domain.Shared;

namespace SBThub.Domain.Shared;

public class ResultResponse
{
    protected internal ResultResponse(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException("Успешный результат не может содержать ошибку.");
            case false when error == Error.None:
                throw new InvalidOperationException("Неуспешный результат обязан содержать ошибку.");
        }

        IsSuccess = isSuccess;
        Error = error;
        Errors = isSuccess ? [] : [error];
    }

    protected internal ResultResponse(bool isSuccess, Error error, Error[] errors)
    {
        IsSuccess = isSuccess;
        Error = error;
        Errors = errors;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public Error[] Errors { get; }

    public static ResultResponse Success() => new(true, Error.None);

    public static ResultResponse<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    public static ResultResponse Failure(Error error) => new(false, error);

    public static ResultResponse Failure(Error error, Error[] errors) => new(false, error, errors);

    public static ResultResponse<TValue> Failure<TValue>(Error error) => new(default, false, error);

    public static ResultResponse<TValue> ValidationFailure<TValue>(Error[] errors) =>
        new(default, false, errors.Length > 0 ? errors[0] : Error.None, errors);
}

public class ResultResponse<TValue> : ResultResponse
{
    private readonly TValue? _value;

    protected internal ResultResponse(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) => _value = value;

    protected internal ResultResponse(TValue? value, bool isSuccess, Error error, Error[] errors)
        : base(isSuccess, error, errors) => _value = value;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Нельзя читать Value у проваленного результата.");
}
