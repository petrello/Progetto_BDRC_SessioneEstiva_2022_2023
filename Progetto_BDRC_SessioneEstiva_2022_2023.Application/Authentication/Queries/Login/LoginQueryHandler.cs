using ErrorOr;
using MediatR;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Common;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Authentication;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Errors;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Entities;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
	{
		private readonly IJwtTokenGenerator jwtTokenGenerator;
		private readonly IUserRepository userRepository;

		public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
		{
			this.jwtTokenGenerator = jwtTokenGenerator;
			this.userRepository = userRepository;
		}

		public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
		{
			if (userRepository.GetUserByEmail(query.Email) is not User user)
			{
				return Errors.Authentication.InvalidCredentials;
			}

			if (user.Password != query.Password)
			{
				// return new[] { Errors.Authentication.InvalidCredentials };
				return Errors.Authentication.InvalidCredentials;
			}

			var token = jwtTokenGenerator.GenerateToken(user);

			return new AuthenticationResult(user, token);
		}
	}
}
