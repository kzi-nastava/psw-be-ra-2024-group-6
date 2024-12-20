using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class RoadTripDto
    {
        public List<LatLngDto> RoadCoords { get; set; }
        public LatLngDto NorthEastCoord { get; set; }
        public LatLngDto SouthWestCoord { get; set; }
    }
}
