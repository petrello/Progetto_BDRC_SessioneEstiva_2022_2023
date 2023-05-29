using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance;

public interface IItineraryRepository : IGenericRepository<Itinerary>
{
	Task Add(Itinerary itinerary);
}
