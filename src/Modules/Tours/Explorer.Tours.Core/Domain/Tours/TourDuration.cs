using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours;

public enum TransportType
{
    Car,
    Bike,
    Walking
}

public class TourDuration : ValueObject
{
    public TimeOnly Duration { get;}
    public TransportType TransportType { get;}

    [JsonConstructor]
    public TourDuration(TimeOnly duration, TransportType transportType)
    {
        Duration = duration;
        TransportType = transportType;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }

    public string ToString()
    {
        return $"{Duration} by {TransportType}";
    }
}
}
