using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Models;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.Entities
{
	public sealed class ItinerarySection : Entity<ItinerarySectionId>
	{
		private readonly List<ItineraryItem> items = new();

		public string Name { get; }
		public string Description { get; }
		public ItinerarySection(ItinerarySectionId id, string name, string description, List<ItineraryItem> items) : base(id)
		{
			Name = name;
			Description = description;
			this.items = items;
		}
		public static ItinerarySection Create(string name, string description, List<ItineraryItem> items)
			=> new(ItinerarySectionId.CreateUnique(), name, description, items);
		public IReadOnlyList<ItineraryItem> Items => items.AsReadOnly();
	}
}
