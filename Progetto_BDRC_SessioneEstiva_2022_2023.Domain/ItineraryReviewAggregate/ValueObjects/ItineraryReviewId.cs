using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.ItineraryReviewAggregate.ValueObjects;

public class ItineraryReviewId : ValueObject
{
    public Guid Value { get; }

    private ItineraryReviewId(Guid value)
    {
        Value = value;
    }
    public static ItineraryReviewId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
