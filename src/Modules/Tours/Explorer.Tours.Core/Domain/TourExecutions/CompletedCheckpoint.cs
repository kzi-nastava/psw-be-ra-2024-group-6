using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class CompletedCheckpoint : ValueObject
    {
        public int CheckpointId { get; init; }
        public DateTime CompletionTime { get; init; }

        [JsonConstructor]
        public CompletedCheckpoint(int checkpointId, DateTime completionTime)
        {
            CheckpointId = checkpointId;
            CompletionTime = completionTime;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CheckpointId;
            yield return CompletionTime;
        }
    }
}
