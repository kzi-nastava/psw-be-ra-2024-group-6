using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class SocialEncounterExecution : EncounterExecution
    {
        public List<int> TouristIds { get; private set; }

        public SocialEncounterExecution()
        {
            TouristIds = new List<int>();
        }

        public SocialEncounterExecution(long encounterId, int touristId)
        {
            EncounterId = encounterId;
            TouristId = touristId;
            TouristIds = new List<int>
            {
                touristId
            };
        }

        public void AddTouristId(int touristId)
        {
            if (!TouristIds.Contains(touristId))
                TouristIds.Add(touristId);
        }

        public bool Complete(int peopleCount)
        {
            if (TouristIds.Count >= peopleCount)
            {
                Status = EncounterExecutionStatus.COMPLETED;
                return true;
            }

            return false;
        }
    }
}
