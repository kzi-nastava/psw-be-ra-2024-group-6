using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos.StatisticDtos
{
    public class TourStatisticsPreviewDto
    {
        public long TourId {  get; set; }
        public string TourName {  get; set; }
        public string TourDescription { get; set; }
        public int StartCount { get; set; }
        public int CompletedCount { get; set; }
        public int Sales {  get; set; }
        public TourStatisticsPreviewDto() { }

        public TourStatisticsPreviewDto(long tourId, string tourName, string tourDescription, int startCount, int completedCount, int sales) {
            TourId = tourId;
            TourName = tourName;
            TourDescription = tourDescription;
            StartCount = startCount;
            CompletedCount = completedCount;
            Sales = sales;
        }


    }
}
