using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourDetailsDto
    {
        public TourDto TourInfo { get; set; } 

        public List<CheckpointReadDto> Checkpoints { get; set; }

        public List<ObjectReadDto> Objects { get; set; }

    }
}
