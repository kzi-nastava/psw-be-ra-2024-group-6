using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourExecutionDatabaseRepository : CrudDatabaseRepository<TourExecution, ToursContext>, ITourExecutionRepository
    {
        public TourExecutionDatabaseRepository(ToursContext dbContext) : base(dbContext) {}
        public TourExecution? GetByIdAndTouristId(int tourExecutionId, int touristId)
        {
            return DbContext.TourExecutions.FirstOrDefault(te => te.Id == tourExecutionId && te.TouristId == touristId);
        }
    }
}
