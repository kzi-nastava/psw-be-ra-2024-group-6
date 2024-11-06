using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;

namespace Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos
{
    public class CheckpointReadDto
    {
        public int Id { get; set; }
        public LocationReadDto Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public string Secret { get; set; }
    }
}
