using Explorer.Tours.Core.Domain.TourExecutions;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourExecutionRepository
{
    public TourExecution Create(TourExecution tourExecution);
    public void Delete(long id);
    public TourExecution Get(long id);
    public TourExecution Update(TourExecution tourExecution);
}