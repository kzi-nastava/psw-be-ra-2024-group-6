using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos;

public class RoadTripExecutionDto
{
    public int Id { get; set; }
    public int RoadTripId { get; set; }
    public int TouristId { get; set; }
    public double Completion { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Status { get; set; }
    public DateTime LastActivity { get; set; }
    public ICollection<int> TourExecutionIds { get; set; }
}
