using ErrorOr;
using MediatR;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Itineraries.Commands; 

public record CreateItineryCommand(
string Name,
string Description,
string HostId,
List<ItinerarySectionCommand> Sections) : IRequest<ErrorOr<Itinerary>>;

public record ItinerarySectionCommand(
	string Name,
	string Description,
	List<ItineraryItemCommand> Items);

public record ItineraryItemCommand(
	string Name,
	string Description);
