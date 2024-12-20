using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.Tours.Core.UseCases.Utils;
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
        yield return Longitude;
        yield return Latitude;
        yield return City;
        yield return Country;
    }

    public string ToString()
    {
        return $"{City}, {Country}";
    }

    public double CalculateDistance(double latitude, double longitude)
    {
        return Utils.CalculateHaversineDistance(Latitude, Longitude, latitude, longitude);
    }
    public static double GetTolerance()
    {
        return 0.02;
    }

    public bool IsLocationSame(Location location)
    {
        return CalculateDistance(location.Latitude, location.Longitude) <= GetTolerance();
    }
}
