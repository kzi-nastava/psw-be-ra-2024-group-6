using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class EncounterReadDto
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public LocationCreateDto Location { get; set; }

        public int Xp { get; set; }

        public string Status { get; set; }
    }
}
