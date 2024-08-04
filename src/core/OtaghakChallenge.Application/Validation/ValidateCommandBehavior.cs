
using FluentValidation;
using MediatR;

namespace OtaghakChallenge.Application.Validation;

public class ValidateCommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest> _validator;

    public ValidateCommandBehavior(IValidator<TRequest> validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator == null)
            return await next();

        FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(request);

        if (validationResult.IsValid)
            return await next();

        string errors = string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

        throw new Exception(errors);
    }
}