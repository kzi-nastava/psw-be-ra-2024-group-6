using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourExecutionDto
    {
        public int TourId { get; set; }
        public int TouristId { get; set; }
        public string Status { get; set; }
        public double Completion { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public TourExecutionDto() {}
    }
}
