using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain
{
    public class TouristRank : Entity
    {
        public int TouristId { get; init; }

        public int Level { get; private set; }

        public int Xp { get; private set; }


        public TouristRank(int touristId, int level, int xp)
        {
            TouristId = touristId;
            Level = level;
            Xp = xp;
        }

        public TouristRank() { }


        public bool CanCreateEncounter()
        {
            return Level >= 10;
        }

        public void AddExperiencePoints(int xp)
        {
            Xp += xp;
            if (Xp >= 1000)
            {
                Level++;
                Xp -= 1000;
            }
        }
    }
}
