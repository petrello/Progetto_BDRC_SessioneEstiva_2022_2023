using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.ValueObjects;

public class ItineraryItemId : ValueObject
{
	public Guid Value { get; }

	private ItineraryItemId(Guid value)
	{
		Value = value;
	}
	public static ItineraryItemId CreateUnique()
	{
		return new(Guid.NewGuid());
	}

	public static ItineraryItemId Create(Guid value)
	{
		return new(value);
	}
	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
