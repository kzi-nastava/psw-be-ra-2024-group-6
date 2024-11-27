using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class TouristRank
    {
        public int TouristId { get; init; }

        public int Level { get; init; }

        public int Xp { get; init; }

        public bool LevelUpXp {  get; init; }

        public TouristRank(int touristId, int level, int xp, bool levelUpXp)
        {
            TouristId = touristId;
            Level = level;
            Xp = xp;
            LevelUpXp = levelUpXp;
        }

        public TouristRank() { }


    }
}
