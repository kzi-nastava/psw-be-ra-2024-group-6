using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos.StatisticDtos
{
    public class AllTourStatisticsDto
    {
        public int Sales {  get; set; }
        public int StartedCount {  get; set; }
        public int CompletedCount {  get; set; }
        public List<CompletionRateStatisticsDto> CompletionRateStats {  get; set; }

        public AllTourStatisticsDto() { }
        public AllTourStatisticsDto(int sales, int startedCount, int completedCount, List<CompletionRateStatisticsDto> completionRateStats)
        {
            Sales = sales;
            StartedCount = startedCount;
            CompletedCount = completedCount;
            CompletionRateStats = completionRateStats;
        }
    }
}
