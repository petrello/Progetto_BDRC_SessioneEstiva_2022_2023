using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Contracts.Itineraries;

public record ItineraryResponse(
	string Id,
	string Name,
	string Description,
	float? AvarageRating,
	List<ItinerarySectionResponse> Sections,
	string HostId,
	List<string> DinnerIds,
	List<string> ItineraryReviewIds,
	DateTime CreatedDateTime,
	DateTime UpdatedDateTime);

public record ItinerarySectionResponse(
	string Id,
	string Name,
	string Description,
	List<ItinerarySectionItem> Items);

public record ItinerarySectionItem(
	string Id,
	string Name,
	string Description);
