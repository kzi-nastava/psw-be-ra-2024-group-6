using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemDto
    {
        public int Id { get; set; }
        public string Category { get;  set; }
        public string Priority { get;  set; }
        public DateTime Date { get;  set; }
        public string Description { get; set; }
        public long TourId { get;  set; }
        public long TouristId { get; set; }
        public bool IsClosed { get; set; }
        public bool IsResolved { get; set; }
        public DateTime DueDate { get; set; }
        public List<ProblemMessageDto> Messages { get; set; }
    }
}
