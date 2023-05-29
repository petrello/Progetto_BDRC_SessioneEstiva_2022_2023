using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Web.Resource;
using Progetto_BDRC_SessioneEstiva_2022_2023.Api.Common.Http;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Api.Controllers.v1
{
	[ApiController]
	[Authorize]
    [RequiredScope("bdrcApi")]
    public class ApiController : ControllerBase
	{
		protected IActionResult Problem(List<Error> errors)
		{
			if (errors.Count is 0) 
				return Problem();
			
			if (errors.All(error => error.Type == ErrorType.Validation))
				return ValidationProblem(errors);

			HttpContext.Items[HttpContextItemsKeys.Errors] = errors;

			return Problem(errors[0]);
		}

		private IActionResult Problem(Error error)
		{
			var statusCode = error.Type switch
			{
				ErrorType.Conflict => StatusCodes.Status409Conflict,
				ErrorType.Validation => StatusCodes.Status400BadRequest,
				ErrorType.NotFound => StatusCodes.Status404NotFound,
				_ => StatusCodes.Status500InternalServerError
			};

			return Problem(statusCode: statusCode, title: error.Description);
		}

		private IActionResult ValidationProblem(List<Error> errors) 
		{
			var modelStateDictionary = new ModelStateDictionary();

			foreach (var error in errors)
			{
				modelStateDictionary.AddModelError(error.Code, error.Description);
			}

			return ValidationProblem(modelStateDictionary);
		}
	}
}
