using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Progetto_BDRC_SessioneEstiva_2022_2023.Api.Common.Errors;
using Progetto_BDRC_SessioneEstiva_2022_2023.Api.Common.Mapping;
using System.Reflection;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Api;

public static class DependencyInjectionExtentsion
{
	public static IServiceCollection AddPresentation(this IServiceCollection services)
	{

		services.AddControllers();

		services.AddSingleton<ProblemDetailsFactory, BDRCProblemDetailsFactory>(); // override the default ProblemDetails

		services.AddMappings();


		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder
                    .WithOrigins("http://localhost:7235") // frontend origin
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
        });

        return services;
	}
}