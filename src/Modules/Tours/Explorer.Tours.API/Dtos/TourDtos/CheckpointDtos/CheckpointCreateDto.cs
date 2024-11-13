using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;

namespace Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos
{
    public class CheckpointCreateDto
    {
        public LocationCreateDto Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public long? TourId { get; set; }
        public string Secret {  get; set; }
    }
}
