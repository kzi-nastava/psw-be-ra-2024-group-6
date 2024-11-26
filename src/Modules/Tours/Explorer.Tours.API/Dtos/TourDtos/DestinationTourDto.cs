using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.DistanceDtos;
using Explorer.Tours.API.Dtos.TourDtos.PriceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class DestinationTourDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public double Price { get; set; }
        public string TotalLength { get; set; }
        public CheckpointReadDto FirstCheckpoint { get; set; }

        public DestinationTourDto() {}
        public DestinationTourDto(string name, string description, string difficulty, double price, string totalLength, CheckpointReadDto firstCheckpoint)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
            Price = price;
            TotalLength = totalLength;
            FirstCheckpoint = firstCheckpoint;
        }
    }
}
