namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces;

public interface ITouristRankRepository
{
    public TouristRank GetByTouristId(int touristId);
}