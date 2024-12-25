using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
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
            var tourExecution = DbContext.TourExecutions.FirstOrDefault(te => te.Id == tourExecutionId && te.TouristId == touristId);
            if (tourExecution == null)
            {
                throw new KeyNotFoundException("Tour execution not found");
            }

            return tourExecution;
        }

        public TourExecution? GetByTourIdAndTouristId(int tourId, int touristId)
        {
            return DbContext.TourExecutions.FirstOrDefault(te => te.TourId == tourId && te.TouristId == touristId && te.Status == TourExecutionStatus.ONGOING); //changed it so that it only finds ongoing tours
        }

        public ICollection<TourExecution> GetByTouristId(int touristId)
        {
            return DbContext.TourExecutions.Where(te => te.TouristId == touristId).ToList();
        }

        public int GetByTourId(int tourId)
        {
            return (int)DbContext.TourExecutions.FirstOrDefault(te => te.TourId == tourId && te.Status == TourExecutionStatus.ONGOING).Id;
        }
    }
}
