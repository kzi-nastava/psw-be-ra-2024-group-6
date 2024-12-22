using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;

namespace Explorer.Tours.API.Dtos.TourDtos.CheckpointDtos
{
    public class CheckpointHoverMapDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageData { get; set; }
        public LocationReadDto Location { get; set; }

    }
}
