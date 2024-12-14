using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class TouristRankDatabaseRepository : CrudDatabaseRepository<TouristRank, EncountersContext>, ITouristRankRepository
    {
        public TouristRankDatabaseRepository(EncountersContext dbContext) : base(dbContext) {}

        public TouristRank GetByTouristId(int touristId)
        {
            var touristRank = DbContext.TouristRanks.FirstOrDefault(tr => tr.TouristId == touristId);
            if (touristRank == null) throw new KeyNotFoundException("Not found: " + touristId);
            return touristRank;
        }
    }
}
