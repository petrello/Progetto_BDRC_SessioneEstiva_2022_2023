using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Models;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.DinnerAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.ValueObjects
{
	public sealed class AverageRating : ValueObject
	{
		public double Value { get; private set; }
		public int NumRatings { get; private set; }
		private AverageRating(double value, int numRatings) 
		{
			Value = value;
			NumRatings = numRatings;
		}
		public static AverageRating Create(double rating = 0, int numRatings = 0)
		{
			return new(rating, numRatings);
		}

		public void AddNewRating(Rating rating)
		{
			Value = ((Value * NumRatings) + rating.Value) / ++NumRatings;
		}

		public void RemoveRating(Rating rating)
		{
			Value = ((Value * NumRatings) - rating.Value) / --NumRatings;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}
