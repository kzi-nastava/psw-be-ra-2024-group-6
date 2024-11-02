using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours;

public enum DistanceUnit
{
    Kilometers,
    Meters,
    Miles,
    Yards

}

public class Distance : ValueObject
{
    public double Length { get;}
    public DistanceUnit Unit { get; }

    [JsonConstructor]
    public Distance(double length, DistanceUnit unit)
    {
        Length = length;
        Unit = unit;
        Validate();
    }
    public void Validate()
    {
        if (Length < 0) throw new ArgumentException("Invalid length.");
    }

    public Distance Add(Distance distance)
    {
        if (Unit != distance.Unit)
        {
            throw new ArgumentException("Cannot add distances with different units.");
        }

        return new Distance(Length + distance.Length, Unit);
    }

    public Distance Subtract(Distance distance)
    {
        if (Unit != distance.Unit)
        {
            throw new ArgumentException("Cannot subtract distances with different units.");
        }

        return new Distance(Length - distance.Length, Unit);
    }

 

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
