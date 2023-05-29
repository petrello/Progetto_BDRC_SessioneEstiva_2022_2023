using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Models;

public abstract class AggregateRootId<TId> : ValueObject
{
	public abstract TId Value { get; protected set; }
}
