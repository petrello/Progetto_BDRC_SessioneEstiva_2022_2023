using System.Net.Http;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Client.Services;
public interface IItineraryHttpClient
{
    Task<HttpClient> GetClient();
}