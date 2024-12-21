using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos.StatisticDtos
{
    public class TourStatisticsDto
    {
        public int StartCount {  get; set; }
        public int CompletionCount {  get; set; }
        public List<CheckpointVisitStatisticsDto> CheckpointVisitStatistics { get; set; }
    }
}
