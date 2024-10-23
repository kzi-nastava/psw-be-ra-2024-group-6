using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class LocationCreateDto
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public LocationCreateDto(long latitude, long longitude)
        {
            
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
