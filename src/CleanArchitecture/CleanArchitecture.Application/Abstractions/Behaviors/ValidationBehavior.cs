using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
  {
    _validators = validators;
  }

  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    if (!_validators.Any())
      return await next();

    var context = new ValidationContext<TRequest>(request);

    var validationErrors = _validators.Select(v => v.Validate(context))
      .Where(r => r.Errors.Count != 0)
      .SelectMany(v => v.Errors)
      .Select(f => new ValidationError(f.PropertyName, f.ErrorMessage))
      .ToList();

    if (validationErrors.Count != 0)
      throw new Exceptions.ValidationException(validationErrors);

    return await next();
  }
}