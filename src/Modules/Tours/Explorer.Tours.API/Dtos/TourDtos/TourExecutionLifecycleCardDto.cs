using Explorer.Tours.API.Dtos.TourDtos.DistanceDtos;
using Explorer.Tours.API.Dtos.TourDtos.DurationDtos;
using Explorer.Tours.API.Dtos.TourDtos.PriceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class TourExecutionLifecycleCardDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public List<string> Tags { get; set; }
        public PriceDto Price { get; set; }
        public string Status { get; set; }
        public long? AuthorId { get; set; }
        public DistanceDto TotalLength { get; set; }
        public DateTime? StatusChangeTime { get; set; }
        public List<TourDurationDto> Durations { get; set; }
        public bool IsPublished { get; set; }
        public List<EquipmentDto> Equipment { get; set; } = new List<EquipmentDto>();
        public string FirstCheckpointImage { get; set; }
    }
}
