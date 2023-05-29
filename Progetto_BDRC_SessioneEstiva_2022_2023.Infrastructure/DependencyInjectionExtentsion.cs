using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Authentication;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Services;
using Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Authentication;
using Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Persistence.Configurations;
using Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Persistence.Repositories;
using Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Persistence.StoredProcedures;
using Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Services;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure;

public static class DependencyInjectionExtentsion
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuth(configuration);
		
		services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

		services.AddPersistance(configuration);

		return services;
	}
	private static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
	{
		var dbSettings = new DatabaseSettings();
		configuration.Bind(DatabaseSettings.SectionName, dbSettings);
		services.AddSingleton(Options.Create(dbSettings));
		
		services.AddSingleton<DapperDbContext>();

		services.AddStoredProcedures(configuration);

		services.AddRepos();

		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}

	private static IServiceCollection AddStoredProcedures(this IServiceCollection services, IConfiguration configuration)
	{
		var itinerarySp = new ItineraryStoredProcedures();
		configuration.Bind(ItineraryStoredProcedures.SectionName, itinerarySp);
		services.AddSingleton(Options.Create(itinerarySp));

		return services;
	}

	private static IServiceCollection AddRepos(this IServiceCollection services)
	{
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IItineraryRepository, ItineraryRepository>();

		return services;
	}

	private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
	{
		var jwtSettings = new JwtSettings();
		configuration.Bind(JwtSettings.SectionName, jwtSettings);
		services.AddSingleton(Options.Create(jwtSettings));

        var certSettings = new CertSettings();
        configuration.Bind(CertSettings.SectionName, certSettings);
        services.AddSingleton(Options.Create(certSettings));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

		services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
				{
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwtSettings.Issuer,
						ValidAudience = jwtSettings.Audience,
						IssuerSigningKey = new X509SecurityKey(new X509Certificate2(certSettings.Location))
					};
				});

        services.AddAuthorization(authOpt =>
        {
            authOpt.AddPolicy("AdminCRUDPolicy", policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.RequireClaim("position", "Administrator");
                policyBuilder.RequireClaim("country", "ITA");
            });
        });

        return services;
	}
}