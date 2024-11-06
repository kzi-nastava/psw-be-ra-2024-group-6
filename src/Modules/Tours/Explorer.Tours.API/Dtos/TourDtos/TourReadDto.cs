using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class TourReadDto
    {
        public TourDto TourInfo { get; set; }

        public List<CheckpointReadDto> Checkpoints { get; set; }

        public List<ObjectReadDto> Objects { get; set; }

    }
}
