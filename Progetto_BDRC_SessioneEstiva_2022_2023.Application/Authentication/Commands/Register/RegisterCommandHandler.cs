using ErrorOr;
using MediatR;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Authentication;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Entities;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Errors;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Common;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
	{
		private readonly IJwtTokenGenerator jwtTokenGenerator;
		private readonly IUserRepository userRepository;

		public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
		{
			this.jwtTokenGenerator = jwtTokenGenerator;
			this.userRepository = userRepository;
		}


		public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
		{
			if (userRepository.GetUserByEmail(command.Email) is not null)
			{
				return Errors.User.DuplicateEmail;
			}

			var user = new User
			{
				FirstName = command.FirstName,
				LastName = command.LastName,
				Email = command.Email,
				Password = command.Password
			};

			userRepository.Add(user);

			// Guid userId = Guid.NewGuid();

			var token = jwtTokenGenerator.GenerateToken(user);

			return new AuthenticationResult(user, token);
		}
	}
}
