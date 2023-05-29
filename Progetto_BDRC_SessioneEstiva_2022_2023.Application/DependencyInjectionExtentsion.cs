using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Commands.Register;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Common;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Behaviors;
using System.Reflection;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application;

public static class DependencyInjectionExtentsion
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		// services.AddScoped<IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>, ValidateRegisterCommandBehavior>();
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		return services;
	}
}