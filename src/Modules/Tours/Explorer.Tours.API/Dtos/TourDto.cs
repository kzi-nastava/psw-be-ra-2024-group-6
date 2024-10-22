using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public enum Status
    {
        Draft,
        Published,
        Closed
    }
    public class TourDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Difficulty Difficulty { get; set; }
        public List<string> Tags { get; set; } = new List<string>(); // Lista tagova koji opisuju turu
        public double Cost { get; set; } = 0; // Default cena je 0
        public Status Status { get; set; } = Status.Draft; // Default status je Draft
        public long AuthorId { get; set; }
    }
}
