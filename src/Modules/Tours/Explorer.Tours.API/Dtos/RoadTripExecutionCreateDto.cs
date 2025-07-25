using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos;

public class RoadTripExecutionCreateDto
{
    public int RoadTripId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}
