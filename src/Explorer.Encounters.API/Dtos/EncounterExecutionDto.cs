using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class EncounterExecutionDto
    {
        public long? Id { get; set; }
        public long EncounterId { get; set; }
        public int TouristId { get; set; }
        public string Status { get; set; }

        public DateTime? TimeOfCompletion { get; set; }
        public EncounterExecutionDto() { }
    }
}
