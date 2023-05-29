using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.HostAggregate.ValueObjects;

public class HostId : ValueObject
{
    public Guid Value { get; }

    private HostId(Guid value)
    {
        Value = value;
    }
    public static HostId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

	public static HostId Create(Guid value)
	{
		return new(value);
	}
	protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
