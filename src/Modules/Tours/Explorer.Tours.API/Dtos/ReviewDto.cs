using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ReviewDto
    {
        public int TouristId { get; set; } 
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime TourDate { get; set; }
        public DateTime ReviewDate { get; set; }
        public List<string> Images { get; set; }
    }
}
