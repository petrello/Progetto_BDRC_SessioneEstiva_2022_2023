using Mapster;
using MapsterMapper;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Api.Common.Mapping
{
	public static class DependencyInjectionExtenstion
	{
		public static IServiceCollection AddMappings(this IServiceCollection services)
		{
			var config = TypeAdapterConfig.GlobalSettings;
			config.Scan(Assembly.GetExecutingAssembly());

			services.AddSingleton(config);
			services.AddScoped<IMapper, ServiceMapper>();

			return services;
		}
	}
}
