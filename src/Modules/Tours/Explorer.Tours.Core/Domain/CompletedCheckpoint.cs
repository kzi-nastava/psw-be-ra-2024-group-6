using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class CompletedCheckpoint
    {
        public int CheckpointId { get; init; }
        public DateTime CompletionTime { get; init; }

        public CompletedCheckpoint(int checkpointId, DateTime completionTime)
        {
            CheckpointId = checkpointId;
            CompletionTime = completionTime;
        }
    }
}
