using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.DistanceDtos;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class RoadTripReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TouristId { get; set; }
        public List<TourDto> Tours { get; set; }
        public List<CheckpointReadDto> PublicCheckpoints { get; set; }
        public string Difficulty { get; private set; }
        public DistanceDto TotalLength { get; private set; }

        public RoadTripReadDto() { }
        public RoadTripReadDto(int id, string name, int touristId, List<TourDto> tours, List<CheckpointReadDto> publicCheckpoints, string difficulty, DistanceDto totalLength)
        {
            Id = id;
            Name = name;
            TouristId = touristId;
            Tours = tours;
            PublicCheckpoints = publicCheckpoints;
            Difficulty = difficulty;
            TotalLength = totalLength;
        }
    }
}
