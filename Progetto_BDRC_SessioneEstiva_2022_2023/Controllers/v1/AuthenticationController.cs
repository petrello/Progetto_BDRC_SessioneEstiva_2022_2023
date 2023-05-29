using Microsoft.AspNetCore.Mvc;
using Progetto_BDRC_SessioneEstiva_2022_2023.Contracts.Authentication;
using ErrorOr;
using Progetto_BDRC_SessioneEstiva_2022_2023.Api.Controllers.v1;
using MediatR;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Commands.Register;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Common;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Queries.Login;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Errors;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Controllers.v1;

[Route("[controller]")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender mediator;
    private readonly IMapper mapper;

	public AuthenticationController(ISender mediator, IMapper mapper)
	{
		this.mediator = mediator;
		this.mapper = mapper;
	}

	[HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        // var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        var command = mapper.Map<RegisterCommand>(request);
        ErrorOr<AuthenticationResult> authResult = await mediator.Send(command);

        return authResult.Match(
            // authResult => Ok(new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token)),
            authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        // var query = new LoginQuery(request.Email, request.Password);
        var query = mapper.Map<LoginQuery>(request);
		ErrorOr<AuthenticationResult> authResult = await mediator.Send(query);

        if(authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }

		return authResult.Match(
			authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
			errors => Problem(errors));
	}
}
