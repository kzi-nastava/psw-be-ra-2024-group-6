using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TransportOptionScoreDto
    {
        public int Id { get; set; }
        public long TourPreferenceId { get; set; }
        public string TransportOption { get; set; }
        public int Score { get; set; }
    }
}
