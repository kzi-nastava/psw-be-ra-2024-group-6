using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours;

public class Location : Entity
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public Location() { }



}
