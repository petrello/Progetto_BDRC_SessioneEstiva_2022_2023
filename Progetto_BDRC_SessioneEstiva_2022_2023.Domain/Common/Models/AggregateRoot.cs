using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Models
{
	public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
		where TId : AggregateRootId<TIdType>
	{
		public new AggregateRootId<TIdType> Id { get; protected set; }

		protected AggregateRoot(TId id) : base(id)
		{
			Id = id;
		}
	}
}
