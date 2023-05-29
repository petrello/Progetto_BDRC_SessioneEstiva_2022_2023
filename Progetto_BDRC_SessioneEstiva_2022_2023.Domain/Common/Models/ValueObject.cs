using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Models
{
	// Basic class
	// All the other value objects in the project will ereditate from this class
	public abstract class ValueObject : IEquatable<ValueObject>
	{
		protected abstract IEnumerable<object> GetEqualityComponents();
		
		protected static bool EqualOperator(ValueObject left, ValueObject right)
		{
			if (left is null ^ right is null)
			{
				return false;
			}
			return ReferenceEquals(left, right) || left!.Equals(right);
		}

		protected static bool NotEqualOperator(ValueObject left, ValueObject right)
		{
			return !(EqualOperator(left, right));
		}

		public static bool operator ==(ValueObject left, ValueObject right)
		{
			return EqualOperator(left, right);
		}

		public static bool operator !=(ValueObject left, ValueObject right)
		{
			return NotEqualOperator(left, right);
		}

		public override int GetHashCode()
		{
			return GetEqualityComponents()
				.Select(x => x?.GetHashCode() ?? 0)
				.Aggregate((x, y) => x ^ y);
		}
		public override bool Equals(object? obj)
		{
			if (obj is null || obj.GetType() != GetType())
			{
				return false;
			}

			var other = (ValueObject)obj;

			return GetEqualityComponents()
				.SequenceEqual(other.GetEqualityComponents());
		}

		public bool Equals(ValueObject? other)
		{
			return Equals((object?)other);
		}
	}
}
