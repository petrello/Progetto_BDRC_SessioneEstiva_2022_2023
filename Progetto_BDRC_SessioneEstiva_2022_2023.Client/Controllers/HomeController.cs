using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Progetto_BDRC_SessioneEstiva_2022_2023.Client.Models;
using Progetto_BDRC_SessioneEstiva_2022_2023.Client.Services;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Client.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly IItineraryHttpClient itineraryHttpClient;
		public HomeController(IItineraryHttpClient itineraryHttpClient)
		{
			this.itineraryHttpClient = itineraryHttpClient;
		}

		public async Task<IActionResult> Index()
		{
			var httpClient = await itineraryHttpClient.GetClient();

			var response = await httpClient.GetAsync("gateway/itineraries").ConfigureAwait(false);

			if (response.IsSuccessStatusCode)
			{
				var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				var itinerararyViewModel = JsonSerializer.Deserialize<List<ItineraryViewModel>>(responseString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!.ToList();

				return View(itinerararyViewModel);
			}
            else if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            throw new Exception($"Problem with fetching data from the API: {response.ReasonPhrase}");
		}

        [HttpGet]
        [Authorize(Policy = "AdminCRUDPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // Specify the type of attribute i.e.
        // it will add the record to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminCRUDPolicy")]
        public async Task<IActionResult> Create(ItineraryViewModel itinerary)
        {
            if (ModelState.IsValid)
            {
                var httpClient = await itineraryHttpClient.GetClient();
                itinerary.Id = Guid.NewGuid();
                var httpContent = new StringContent(JsonSerializer.Serialize(itinerary), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"gateway/itineraries", httpContent).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                throw new Exception($"Problem with fetching data from the API: {response.ReasonPhrase}");
            }
            return View(itinerary);
        }

        [HttpGet]
        [Authorize(Policy = "AdminCRUDPolicy")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var httpClient = await itineraryHttpClient.GetClient();
            
            var response = await httpClient.GetAsync($"gateway/itineraries/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var itinerararyViewModel = JsonSerializer.Deserialize<ItineraryViewModel>(responseString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(itinerararyViewModel);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            throw new Exception($"Problem with fetching data from the API: {response.ReasonPhrase}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminCRUDPolicy")]
        public async Task<IActionResult> Edit(ItineraryViewModel itinerary)
        {
            if (ModelState.IsValid)
            {
                var httpClient = await itineraryHttpClient.GetClient();
                var httpContent = new StringContent(JsonSerializer.Serialize(itinerary), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"gateway/itineraries", httpContent).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception($"Problem with editing data from the API: {response.ReasonPhrase}");
            }
            return View(itinerary);
        }

        [HttpGet]
        [Authorize(Policy = "AdminCRUDPolicy")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var httpClient = await itineraryHttpClient.GetClient();
            var response = await httpClient.DeleteAsync($"gateway/itineraries/{id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            else
                throw new Exception($"Problem with deleting data from the API: {response.ReasonPhrase}");
        }

        // [Authorize(Roles = "Admin")]
        [Authorize(Policy = "AdminCRUDPolicy")]
        public async Task<IActionResult> Privacy()
		{
            var client = new HttpClient();
            var metaDataResponse = await client.GetDiscoveryDocumentAsync("https://localhost:7009");
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var response = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = metaDataResponse.UserInfoEndpoint,
                Token = accessToken
            });
            if (response.IsError)
            {
                throw new Exception("Problem while fetching data from the UserInfo endpoint", response.Exception);
            }

            return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}