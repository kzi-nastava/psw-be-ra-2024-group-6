using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class HiddenEncounterDto : EncounterCreateDto
    {
        public int Range { get; set; }
        public LocationDto HiddenLocation { get; set; }
        public string ImageUrl { get; set; }

        public HiddenEncounterDto()
        {

        }

    }
}
