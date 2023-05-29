using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Contracts.Itineraries;

public record CreateItineraryRequest(
	string Name,
	string Description,
	List<ItinerarySection> Sections);

public record ItinerarySection(
	string Name,
	string Description,
	List<ItineraryItem> Items);

public record ItineraryItem(
	string Name,
	string Description);
