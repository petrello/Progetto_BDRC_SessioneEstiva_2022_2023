using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Models;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.ValueObjects;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.DinnerAggregate.ValueObjects;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.GuestAggregate.ValueObjects;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.HostAggregate.ValueObjects;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.Entities;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.ValueObjects;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.ItineraryReviewAggregate.ValueObjects;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary;

public sealed class Itinerary : AggregateRoot<ItineraryId, Guid>
{
	private readonly List<ItinerarySection> sections = new();
	private readonly List<DinnerId> dinnerIds = new();
	private readonly List<ItineraryReviewId> itineraryReviewIds = new();

	public string Name { get; }
	public string Description { get; }
	public AverageRating AverageRating { get; }
	public DateTime CreatedDateTime { get; }
	public DateTime UpdatedDateTime{ get; }
	public IReadOnlyList<ItinerarySection> Sections => sections.AsReadOnly();
	public HostId HostId { get; }
	public IReadOnlyList<DinnerId> DinnerIds => dinnerIds.AsReadOnly();
	public IReadOnlyList<ItineraryReviewId> ItineraryReviewIds => itineraryReviewIds.AsReadOnly();

	private Itinerary(
		ItineraryId id,
		string name,
		string description,
		HostId hostId,
		List<ItinerarySection> sections,
		DateTime createdDateTime,
		DateTime updatedDateTime) : base(id)
	{
		Name = name;
		Description = description;
		HostId = hostId;
		AverageRating = AverageRating.Create();
		this.sections = sections;
		CreatedDateTime = createdDateTime;
		UpdatedDateTime = updatedDateTime;
	}

	public static Itinerary Create(
		string name,
		string description,
		HostId hostId,
		List<ItinerarySection> sections)
		=> new(
			ItineraryId.CreateUnique(),
			name,
			description,
			hostId,
			sections,
			DateTime.UtcNow,
			DateTime.UtcNow);
}
