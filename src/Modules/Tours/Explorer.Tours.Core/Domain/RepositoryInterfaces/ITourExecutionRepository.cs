using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourExecutionRepository
{
    public TourExecution Create(TourExecution tourExecution);
    public void Delete(long id);
    public TourExecution Get(long id);
    public TourExecution? GetByIdAndTouristId(int tourExecutionId, int touristId);
    public TourExecution? GetByTourIdAndTouristId(int tourId, int touristId);
    public TourExecution Update(TourExecution tourExecution);
    public ICollection<TourExecution> GetByTouristId(int touristId);
}