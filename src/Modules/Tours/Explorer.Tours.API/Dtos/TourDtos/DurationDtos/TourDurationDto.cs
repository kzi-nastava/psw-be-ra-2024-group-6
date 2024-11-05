using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos.DurationDtos
{
    public class TourDurationDto
    {
        public TimeOnly Duration { get; set; }
        public string TransportType { get; set; }
    }
}
