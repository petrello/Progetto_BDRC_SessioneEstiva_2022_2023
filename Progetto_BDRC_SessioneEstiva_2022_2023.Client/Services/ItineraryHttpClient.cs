using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http.Headers;
using System.Net.Http;
using IdentityModel.Client;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Client.Services
{
    public class ItineraryHttpClient : IItineraryHttpClient
	{
        private readonly IHttpContextAccessor httpContextAccessor;
        private HttpClient httpClient;

        public ItineraryHttpClient(IHttpContextAccessor httpContextAccessor)
        {
            httpClient = new HttpClient();
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpClient> GetClient()
        {
            var accessToken = await httpContextAccessor
                .HttpContext!
                .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                httpClient.SetBearerToken(accessToken);
            }

            httpClient.BaseAddress = new Uri("https://localhost:5003/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            return httpClient;
        }
    }
}
