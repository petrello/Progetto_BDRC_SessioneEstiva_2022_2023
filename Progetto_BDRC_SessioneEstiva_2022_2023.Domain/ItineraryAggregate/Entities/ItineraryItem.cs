using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Models;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.Entities
{
	public class ItineraryItem : Entity<ItineraryItemId>
	{
		public string Name { get; }
		public string Description { get; }
		private ItineraryItem(ItineraryItemId id, string name, string description) : base(id)
		{
			Name = name;
			Description = description;
		}
		public static ItineraryItem Create(string name, string description)
			=> new(ItineraryItemId.CreateUnique(), name, description);
	}
}
