using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface IRoadTripRepository
{
    public RoadTrip Create(RoadTrip roadTrip);
    public List<RoadTrip> GetAllByTouristId(int touristId);
    public RoadTrip Get(long id);
}