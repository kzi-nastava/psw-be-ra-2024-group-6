using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos.StatisticDtos
{
    public class TourStatisticsDto
    {
        public TourStatisticsPreviewDto TourStatistics {  get; set; }
        public List<CheckpointVisitStatisticsDto> CheckpointVisitStatistics { get; set; }

        public TourStatisticsDto() { }
        public TourStatisticsDto(int startCount, int completedCount, int sales, List<CheckpointVisitStatisticsDto> checkpointVisitStatistics, TourReadDto tour)
        {
            TourStatistics = new TourStatisticsPreviewDto();
            TourStatistics.StartCount = startCount;
            TourStatistics.CompletedCount = completedCount;
            TourStatistics.Sales = sales;
            CheckpointVisitStatistics = checkpointVisitStatistics;
            TourStatistics.TourId = tour.TourInfo.Id ?? -1;
            TourStatistics.TourName = tour.TourInfo.Name;
            TourStatistics.TourDescription = tour.TourInfo.Description;
        }
    }
}
