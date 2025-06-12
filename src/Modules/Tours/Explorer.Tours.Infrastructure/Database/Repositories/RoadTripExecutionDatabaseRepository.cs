using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class RoadTripExecutionDatabaseRepository : CrudDatabaseRepository<RoadTripExecution, ToursContext>, IRoadTripExecutionRepository
{
    private readonly ToursContext _context;

    public RoadTripExecutionDatabaseRepository(ToursContext context) : base(context)
    {
        _context = context;
    }

    public int GetTouristId(long roadTripExecutionId)
    {
        var entity = _context.RoadTripExecutions.FirstOrDefault(rte => rte.Id == roadTripExecutionId);
        return entity.TouristId;
    }

    public ICollection<RoadTripExecution> GetByTouristId(int touristId)
    {
        return _context.RoadTripExecutions.Where(rte => rte.TouristId == touristId).ToList();
    }

    public RoadTripExecution GetByIdAndTouristId(int roadTripExecutionId, int touristId)
    {
        var roadTripExecution = _context.RoadTripExecutions.FirstOrDefault(rte => rte.Id == roadTripExecutionId && rte.TouristId == touristId);
        return roadTripExecution;
    }

    public Boolean IsOneStarted()
    {
        var result = _context.RoadTripExecutions.Any(rte => rte.Status == RoadtripExecutionStatus.ONGOING);
        return result;
    }
}
