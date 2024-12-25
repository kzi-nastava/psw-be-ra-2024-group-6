using Explorer.Tours.Core.Domain.TourExecutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface IRoadTripExecutionRepository
{
    public RoadTripExecution Create(RoadTripExecution roadTripExecution);
    public void Delete(long id);
    public RoadTripExecution Get(long id);
    public RoadTripExecution Update(RoadTripExecution roadTripExecution);
    public int GetTouristId(long roadTripExecutionId);
    public ICollection<RoadTripExecution> GetByTouristId(int touristId);
    public RoadTripExecution GetByIdAndTouristId(int roadTripExecutionId, int touristId);
    public Boolean IsOneStarted();
}
