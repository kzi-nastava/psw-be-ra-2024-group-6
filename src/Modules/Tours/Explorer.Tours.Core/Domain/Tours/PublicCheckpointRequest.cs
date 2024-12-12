using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours
{
    public enum PublicCheckpointStatus
    {
        Pending,
        Rejected, 
        Approved
    }


    public class PublicCheckpointRequest : Entity
    {
        public long CheckpointId { get; private set; }
        public PublicCheckpointStatus Status { get; private set; }
        public string? AdminComment { get; private set; }

        public PublicCheckpointRequest(long checkpointId)
        {
            CheckpointId = checkpointId;
            Status = PublicCheckpointStatus.Pending;
        }

        public void Approve()
        {
            Status = PublicCheckpointStatus.Approved;
            AdminComment = null;
        }

        public void Reject(string comment)
        {
            Status = PublicCheckpointStatus.Rejected;
            AdminComment = comment;
        }
    }
}
