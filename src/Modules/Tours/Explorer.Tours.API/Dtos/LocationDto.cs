using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class LocationDto
    {
        public long Id { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
    }
}
