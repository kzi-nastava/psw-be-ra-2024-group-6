using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public enum TourExecutionStatus
    {
        ONGOING,
        ABANDONED,
        COMPLETED
    };

    public class TourExecution : Entity
    {
        public int TourId { get; init; }
        public int TouristId { get; init; }
        public TourExecutionStatus Status { get; private set; }
        public DateTime LastActivity { get; private set; }
        public double Completion { get; private set; }
        public TouristPosition Position { get; private set; }
        public ICollection<CompletedCheckpoint> CompletedCheckpoints { get; init; }

        public TourExecution(int tourId, int touristId, double longitude, double latitude)
        {
            Position = new TouristPosition(longitude, latitude);
            CompletedCheckpoints = new List<CompletedCheckpoint>();
            TourId = tourId;
            TouristId = touristId;
            Status = TourExecutionStatus.ONGOING;
            LastActivity = DateTime.UtcNow;
            Completion = 0;
        }

        public TourExecution() {}

        public void Finalize(string status)
        {
            if (Enum.TryParse<TourExecutionStatus>(status, true, out var parsedStatus))
            {
                Status = parsedStatus;
                LastActivity = DateTime.UtcNow;
            }
            else
            {
                throw new ArgumentException($"Invalid status value: {status}");
            }
        }
    }
}
