using ErrorOr;
using MediatR;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Common;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Queries.Login;
public record LoginQuery(
	string Email,
	string Password) : IRequest<ErrorOr<AuthenticationResult>>;

