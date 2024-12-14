using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class PublicCheckpointRequestDto
    {
        public string? AdminComment { get; set; } 
        public string Status { get; set; } 
    }
}
