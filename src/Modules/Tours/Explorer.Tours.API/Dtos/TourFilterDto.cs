using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.DistanceDtos;
using Explorer.Tours.API.Dtos.TourDtos.DurationDtos;

namespace Explorer.Tours.API.Dtos
{
    public class TourFilterDto
    {
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string? Name { get; set; }
        public string? Tag { get; set; }
        public double? MinLength { get; set; }
        public double? MaxLength { get; set; }
        public TourDurationDto? MinDuration { get; set; }
        public TourDurationDto? MaxDuration { get; set; }
    }
}
