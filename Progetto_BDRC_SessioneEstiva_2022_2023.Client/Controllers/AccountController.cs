using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Client.Controllers
{
	public class AccountController : Controller
	{
		public async Task Logout()
		{
			await HttpContext.SignOutAsync("Cookies");
			await HttpContext.SignOutAsync("oidc");
		}

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
