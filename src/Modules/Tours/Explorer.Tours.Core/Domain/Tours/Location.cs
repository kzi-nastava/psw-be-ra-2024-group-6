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
    public double CalculateDistance(double latitude, double longitude)
    {
        const double R = 6371; // Poluprečnik Zemlje u kilometrima
        double dLat = DegreesToRadians(Latitude - latitude);
        double dLon = DegreesToRadians(Longitude - longitude);

        // Haversine formula
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(DegreesToRadians(latitude)) * Math.Cos(DegreesToRadians(Latitude)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return R * c; // Rezultat u kilometrima
    }

    public static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}
