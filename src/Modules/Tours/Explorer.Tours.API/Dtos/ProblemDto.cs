using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ProblemDto
    {
        public string Category { get;  set; }
        public string Priority { get;  set; }
        public DateTime Date { get;  set; }
        public string Description { get; set; }
        public long TourId { get;  set; }
        public long TouristId { get; set; }

    }
}
