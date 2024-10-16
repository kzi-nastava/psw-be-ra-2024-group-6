using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourPreferenceDto
    {
        public List<TransportOptionScoreDto> TransportOptionScores { get; set; }
        public int TourId { get; set; }
        public long Id { get; set; }
        public string Difficulty { get; set; }
        public List<string> Tags { get; set; }
    }
}
