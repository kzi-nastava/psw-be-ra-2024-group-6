using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class TouristRankService : ITouristRankService
    {
        private readonly ITouristRankRepository _touristRankRepository;

        public TouristRankService(ITouristRankRepository touristRankRepository)
        {
            _touristRankRepository = touristRankRepository;
        }

        public Result<bool> CanCreateEncounter(int touristId)
        {
            try
            {
                var touristRank = _touristRankRepository.GetByTouristId(touristId);
                return touristRank.CanCreateEncounter();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public void AddExperiencePoints(int userId, int xp)
        {
            var touristRank = _touristRankRepository.GetByTouristId(userId);
            touristRank.AddExperiencePoints(xp);
            _touristRankRepository.Update(touristRank);
        }
    }
}
