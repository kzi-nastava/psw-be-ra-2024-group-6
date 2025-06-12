using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Explorer.Tours.Core.Domain.TourExecutions;
public enum RoadtripExecutionStatus
{
    ONGOING,
    ABANDONED,
    COMPLETED
};
public class RoadTripExecution : Entity
{
    public int TouristId { get; init; }
    public int RoadTripId { get; init; }
    public ICollection<int> TourExecutionIds { get; set; }
    public double Completion { get; private set; }
    public TouristPosition Position { get; private set; }
    public RoadtripExecutionStatus Status { get; private set; }
    public DateTime LastActivity { get; private set; }
    public ICollection<CompletedCheckpoint> CompletedPublicCheckpoints { get; init; }

    public RoadTripExecution() { }

    public RoadTripExecution(int touristId, int roadTripId, double completion, double longitude, double latitude)
    {
        if (!Validate(longitude, latitude))
        {
            throw new ArgumentException("Invalid longitude or latitude values");
        }
        TouristId = touristId;
        RoadTripId = roadTripId;
        TourExecutionIds = new List<int>();
        CompletedPublicCheckpoints = new List<CompletedCheckpoint>();
        Completion = completion;
        Position = new TouristPosition(longitude, latitude);
        Status = RoadtripExecutionStatus.ONGOING;
        Completion = 0;
        LastActivity = DateTime.UtcNow;
    }

    public bool Validate(double longitude, double latitude)
    {
        return longitude is >= -180 and <= 180 && latitude is >= -90 and <= 90;
    }

    public void Finalize(string status)
    {
        if (Enum.TryParse<RoadtripExecutionStatus>(status, true, out var parsedStatus))
        {
            Status = parsedStatus;
            LastActivity = DateTime.UtcNow;
        }
        else
        {
            throw new ArgumentException($"Invalid status value: {status}");
        }
    }

    public void SetLastActivity(double longitude, double latitude)
    {
        this.LastActivity = DateTime.UtcNow;
        this.Position = new TouristPosition(longitude, latitude);
    }

    public void CalculateCompletion(int completionCount)
    {
        this.Completion = Math.Round((double)completionCount / TourExecutionIds.Count * 100, 2);
    }
}
