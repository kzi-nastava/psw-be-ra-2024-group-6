using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourCreateDto
    {
        public TourInfoDto TourInfo { get; set; }

        public List<CheckpointCreateDto> Checkpoints { get; set; }

        public List<ObjectCreateDto> Objects { get;set; }
    }
}
