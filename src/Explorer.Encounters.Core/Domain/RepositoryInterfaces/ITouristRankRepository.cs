using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces;

public interface ITouristRankRepository : ICrudRepository<TouristRank>
{
    public TouristRank GetByTouristId(int touristId);
}