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

    public RoadTripExecution GetByRoadTripId(long roadTripId)
    {
        var entity = _context.RoadTripExecutions.FirstOrDefault(rte => rte.RoadTripId == roadTripId);
        return entity;
    }
}
