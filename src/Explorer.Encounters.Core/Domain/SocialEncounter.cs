using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class SocialEncounter : Encounter
    {
        public int PeopleCount { get; private set; }
        public int Radius { get; private set; }

        public SocialEncounter() { }
        public SocialEncounter(string name, string description, Location loc, int xp, Status status, TypeEncounter type, int peopleCount, int radius, int creatorId, bool isRequired) : base(name, description, loc, xp, status, type,creatorId,isRequired)
        {
            PeopleCount = peopleCount;
            Radius = radius;
        }
    }
}
