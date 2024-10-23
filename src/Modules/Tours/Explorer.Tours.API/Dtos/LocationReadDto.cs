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


        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LocationReadDto(long id, double latitude, double longitude) {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }



    }
}
