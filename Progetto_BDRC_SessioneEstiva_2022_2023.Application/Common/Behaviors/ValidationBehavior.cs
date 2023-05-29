using ErrorOr;
using FluentValidation;
using MediatR;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Commands.Register;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Common;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : 
	IPipelineBehavior<TRequest, TResponse> 
	where TRequest : IRequest<TResponse>
	where TResponse : IErrorOr
{
	private readonly IValidator<TRequest>? validator;

	public ValidationBehavior(IValidator<TRequest>? validator = null)
	{
		this.validator = validator;
	}

	public async Task<TResponse> Handle(
		TRequest request, 
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		if(validator is null)
		{
			return await next();
		}
		
		var validationResult = await validator.ValidateAsync(request, cancellationToken);
		if (validationResult.IsValid)
		{
			return await next();
		}

		var errors = validationResult.Errors
			.ConvertAll(validationFailure => Error.Validation(
				validationFailure.PropertyName, 
				validationFailure.ErrorMessage));

		return (dynamic)errors;
	}
}
