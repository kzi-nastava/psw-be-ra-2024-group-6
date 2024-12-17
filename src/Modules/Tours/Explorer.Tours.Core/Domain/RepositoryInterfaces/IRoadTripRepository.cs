using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface IRoadTripRepository
{
    public RoadTrip Create(RoadTrip roadTrip);
}