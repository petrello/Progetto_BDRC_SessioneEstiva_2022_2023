using ErrorOr;
using MediatR;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.HostAggregate.ValueObjects;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.Entities;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Itineraries.Commands;

public class CreateItineryCommandHandler : IRequestHandler<CreateItineryCommand, ErrorOr<Itinerary>>
{
	// private readonly IItineraryRepository itineraryRepository;
	private readonly IUnitOfWork unitOfWork;

	public CreateItineryCommandHandler(IUnitOfWork unitOfWork)
	{
		this.unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<Itinerary>> Handle(CreateItineryCommand request, CancellationToken cancellationToken)
	{
		var itinerary = Itinerary.Create(
			hostId: HostId.CreateUnique(),
			name: request.Name,
			description: request.Description,
			sections: request.Sections.ConvertAll(section => ItinerarySection.Create(
				name: section.Name,
				description: section.Description,
				items: section.Items.ConvertAll(item => ItineraryItem.Create(
					name: item.Name,
					description: item.Description)))));

		await unitOfWork.Itineraries.Add(itinerary);

		return itinerary;
	}
}
