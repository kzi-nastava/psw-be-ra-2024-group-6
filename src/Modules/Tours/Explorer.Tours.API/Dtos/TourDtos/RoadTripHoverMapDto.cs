using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointDtos;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class RoadTripHoverMapDto
    {
        public List<TourHoverMapDto> Tours { get; set; }
        public List<CheckpointHoverMapDto> PublicCheckpoints { get; set; }

        public RoadTripHoverMapDto(List<TourHoverMapDto> tours, List<CheckpointHoverMapDto> publicCheckpoints)
        {
            Tours = tours;
            PublicCheckpoints = publicCheckpoints;
        }
    }
}
