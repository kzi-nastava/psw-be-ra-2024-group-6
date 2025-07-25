using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public;

public interface ITouristRankService
{
    public Result<bool> CanCreateEncounter(int touristId);
    public void AddExperiencePoints(int userId, int xp);
    public TouristRankDto GetTouristRank(int userId);
}