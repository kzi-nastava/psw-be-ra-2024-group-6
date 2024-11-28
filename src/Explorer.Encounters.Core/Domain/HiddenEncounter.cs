using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class HiddenEncounter : Encounter
    {
        public int Range {get; private set; }
        public Location HiddenLocation { get; private set; }
        public string ImageUrl { get; private set; }

        public HiddenEncounter()
        {
        }
        public HiddenEncounter(string name, string description, Location loc, int xp, Status status, TypeEncounter type,
            int range, Location hiddenLocation, string imageUrl,int creatorId,bool isRequiredForCheckpoint) : base(name, description, loc, xp, status, type,creatorId,isRequiredForCheckpoint)
        {
            Range = range;
            HiddenLocation = hiddenLocation;
            ImageUrl = imageUrl;

        }
    }
}
