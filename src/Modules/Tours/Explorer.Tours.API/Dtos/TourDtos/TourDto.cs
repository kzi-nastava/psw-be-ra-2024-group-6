using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class TourDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public double Cost { get; set; } = 0;
        public string Status { get; set; } = "Draft";
        public long? AuthorId { get; set; }
    }
}
