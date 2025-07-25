using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class TourCreateDto
    {
        public TourDto TourInfo { get; set; }
        public List<CheckpointCreateDto>? Checkpoints { get; set; }
        public List<ObjectCreateDto>? Objects { get; set; }
    }
}
