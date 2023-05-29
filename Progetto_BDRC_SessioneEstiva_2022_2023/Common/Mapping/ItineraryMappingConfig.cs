using Mapster;

using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Itineraries.Commands;
using Progetto_BDRC_SessioneEstiva_2022_2023.Contracts.Itineraries;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary;

using ItinerarySection = Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.Entities.ItinerarySection;
using ItineraryItem = Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.Entities.ItineraryItem;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Api.Common.Mapping;

public class ItineraryMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<(CreateItineraryRequest Request, string HostId), CreateItineryCommand>()
			.Map(dest => dest.HostId, src => src.HostId)
			.Map(dest => dest, src => src.Request);

		config.NewConfig<Itinerary, ItineraryResponse>()
			.Map(dest => dest.Id, src => src.Id.Value)
			.Map(dest => dest.AvarageRating, src => src.AverageRating.Value)
			.Map(dest => dest.HostId, src => src.HostId)
			.Map(dest => dest.DinnerIds, src => src.DinnerIds.Select(dinnerId => dinnerId.Value))
			.Map(dest => dest.ItineraryReviewIds, src => src.ItineraryReviewIds.Select(itineraryReviewId => itineraryReviewId.Value));

		config.NewConfig<ItinerarySection, ItinerarySectionResponse>()
			.Map(dest => dest.Id, src => src.Id.Value);

		config.NewConfig<ItineraryItem, ItinerarySectionResponse>()
			.Map(dest => dest.Id, src => src.Id.Value);
	}
}
