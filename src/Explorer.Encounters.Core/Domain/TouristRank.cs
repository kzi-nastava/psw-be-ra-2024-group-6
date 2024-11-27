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

        public int Level { get; init; }

        public int Xp { get; init; }

        public int LevelUpXp {  get; init; }

        public TouristRank(int touristId, int level, int xp, int levelUpXp)
        {
            TouristId = touristId;
            Level = level;
            Xp = xp;
            LevelUpXp = levelUpXp;
        }

        public TouristRank() { }


        public bool CanCreateEncounter()
        {
            return Level >= 10;
        }
    }
}
