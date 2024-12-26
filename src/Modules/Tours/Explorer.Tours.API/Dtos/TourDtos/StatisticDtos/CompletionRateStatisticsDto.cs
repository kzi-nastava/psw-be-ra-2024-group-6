using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos.StatisticDtos
{
    public class CompletionRateStatisticsDto
    {
        public int Count {  get; set; }
        public string Percentage {  get; set; }

        public CompletionRateStatisticsDto(int count, string completionPercentage)
        {
            Count = count;
            Percentage = completionPercentage;
        }

        public CompletionRateStatisticsDto() { }
    }
}
