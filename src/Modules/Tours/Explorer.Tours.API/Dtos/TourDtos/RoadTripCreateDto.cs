using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class RoadTripCreateDto
    {
        public string Name { get; set; }
        public List<int> TourIds { get; set; }
        public List<int> PublicCheckpointIds { get; set; }
    }
}
