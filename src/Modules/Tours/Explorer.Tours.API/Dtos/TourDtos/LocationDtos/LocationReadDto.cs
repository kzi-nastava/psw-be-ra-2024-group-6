using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos.LocationDtos
{
    public class LocationReadDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

    }
}
