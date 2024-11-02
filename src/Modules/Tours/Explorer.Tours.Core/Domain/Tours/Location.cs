using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours;

public class Location : ValueObject
{
    public double Latitude { get; }
    public double Longitude { get; }
    public string Country { get; }
    public string City { get; }

    [JsonConstructor]
    public Location(double latitude, double longitude, string country, string city)
    {
        Latitude = latitude;
        Longitude = longitude;
        Country = country;
        City = city;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }

    public string ToString()
    {
        return $"{City}, {Country}";
    }
}
}
