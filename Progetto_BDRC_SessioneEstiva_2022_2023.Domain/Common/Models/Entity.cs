using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Models
{
	public abstract class Entity<TId> : IEquatable<Entity<TId>> 
		where TId : notnull
	{
		public TId Id { get; protected set; }

		protected Entity(TId id)
		{
			Id = id;
		}

		public override bool Equals(object? obj)
		{
			return obj is Entity<TId> entity && Id.Equals(entity.Id);
		}

		protected static bool EqualOperator(Entity<TId> left, Entity<TId> right)
		{
			if (left is null ^ right is null)
			{
				return false;
			}
			return ReferenceEquals(left, right) || left!.Equals(right);
		}

		protected static bool NotEqualOperator(Entity<TId> left, Entity<TId> right)
		{
			return !(EqualOperator(left, right));
		}

		public static bool operator ==(Entity<TId> left, Entity<TId> right)
		{
			return EqualOperator(left, right);
		}

		public static bool operator !=(Entity<TId> left, Entity<TId> right)
		{
			return NotEqualOperator(left, right);
		}

		public bool Equals(Entity<TId>? other)
		{
			return Equals((object?)other);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
