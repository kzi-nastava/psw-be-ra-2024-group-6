using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

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
        Validate();
    }

    private void Validate()
    {
        if (Latitude < -90 || Latitude > 90) throw new ArgumentException("Invalid Latitude.");
        if (Longitude < -180 || Longitude > 180) throw new ArgumentException("Invalid Longitude.");
        if (string.IsNullOrWhiteSpace(Country)) throw new ArgumentException("Invalid Country.");
        if (string.IsNullOrWhiteSpace(City)) throw new ArgumentException("Invalid City.");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
