using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ReviewDto
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public long TourId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime TourDate { get; set; }
        public DateTime ReviewDate { get; set; }
        public List<string> Images { get; set; }
        public double Completion {  get; set; }
    }
}
