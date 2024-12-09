using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class SocialEncounterExecutionStatusDto
    {
        public int PeopleCount { get; set; }
        public bool IsCompleted { get; set; }

        public SocialEncounterExecutionStatusDto(int peopleCount, bool isCompleted)
        {
            PeopleCount = peopleCount;
            IsCompleted = isCompleted;
        }
    }
}
