using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class TourMapDto
    {
        public LocationReadDto Location { get; set; }

        public TourMapDto(LocationReadDto location)
        {
            Location = location;
        }
    }
}
