using Mapster;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Commands.Register;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Common;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Queries.Login;
using Progetto_BDRC_SessioneEstiva_2022_2023.Contracts.Authentication;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Api.Common.Mapping
{
	public class AuthenticationMappingConfig : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<RegisterRequest, RegisterCommand>();

			config.NewConfig<LoginRequest, LoginQuery>();
			
			config.NewConfig<AuthenticationResult, AuthenticationResponse>()
				// .Map(dest => dest.Token, src => src.Token) // no need this map 'cause same name prop
				.Map(dest => dest, src => src.User);
		}
	}
}
