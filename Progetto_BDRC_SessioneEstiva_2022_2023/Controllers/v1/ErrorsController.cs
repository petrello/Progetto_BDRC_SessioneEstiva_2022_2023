using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Exceptions;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Api.Controllers.v1
{
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorsController : ControllerBase
	{
		[Route("/error-development")]
		public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
		{
			if (!hostEnvironment.IsDevelopment())
			{
				return NotFound();
			}

			Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

			var (statusCode, message) = exception switch
			{
				_ => (StatusCodes.Status500InternalServerError, "An unexpected error occured.")
			};

			return Problem(
				title: message,
				statusCode: statusCode);
		}

		[Route("/error")]
		public IActionResult HandleError() =>
			Problem();
	}
}
