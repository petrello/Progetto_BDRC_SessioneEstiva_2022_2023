using ErrorOr;
using MediatR;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Common;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Commands.Register;
public record RegisterCommand(
	string FirstName,
	string LastName,
	string Email,
	string Password) : IRequest<ErrorOr<AuthenticationResult>>;
