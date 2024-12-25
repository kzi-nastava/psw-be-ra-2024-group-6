using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos.StatisticDtos
{
    public class CheckpointVisitStatisticsDto
    {
        public CheckpointReadDto Checkpoint { get; set; }
        public double VisitPercentage {  get; set; }

        public CheckpointVisitStatisticsDto() { }

        public CheckpointVisitStatisticsDto(CheckpointReadDto checkpoint, double visitPercentage)
        {
            Checkpoint = checkpoint;
            VisitPercentage = visitPercentage;
        }
    }
}
