using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public enum EncounterExecutionStatus
    {
        STARTED,
        COMPLETED,
        ABONDONED
    };
    public class EncounterExecution : Entity
    {
        public int EncounterId { get; init; }
        public int touristId { get; init; }
        public EncounterExecutionStatus Status { get; private set; }

        public DateTime TimeOfCompletion { get;private set; }


        public EncounterExecution(int encounterId, int touristId)
        {
            EncounterId = encounterId;
            this.touristId = touristId;
            Status = EncounterExecutionStatus.STARTED;
            TimeOfCompletion = DateTime.UtcNow;
            Validate();
        }

        public EncounterExecution() { }


        public bool Validate()
        {
            return true;
        }
    }
}
