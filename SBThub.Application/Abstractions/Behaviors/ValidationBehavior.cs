using FluentValidation;
using SBThub.Domain.Shared;
using MediatR;

namespace SBThub.Application.Abstractions.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : ResultResponse
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        Error[] errors = validators
            .Select(validator => validator.Validate(context))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(failure => failure is not null)
            .Select(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Length == 0)
            return await next();

        return CreateValidationFailure(errors);
    }

    private static TResponse CreateValidationFailure(Error[] errors)
    {
        if (typeof(TResponse) == typeof(ResultResponse))
            return (TResponse)(object)ResultResponse.Failure(errors[0], errors);

        var valueType = typeof(TResponse).GenericTypeArguments[0];

        var validationFailure = typeof(ResultResponse)
            .GetMethod(nameof(ResultResponse.ValidationFailure))!
            .MakeGenericMethod(valueType)
            .Invoke(null, [errors])!;

        return (TResponse)validationFailure;
    }
}
