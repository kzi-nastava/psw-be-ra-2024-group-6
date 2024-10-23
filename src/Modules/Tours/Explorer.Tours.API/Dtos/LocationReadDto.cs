using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class LocationReadDto
    {
        public long Id { get; set; }

        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public LocationReadDto(long id,long latitude,long longitude) {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
