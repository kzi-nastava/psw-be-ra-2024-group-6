using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Execution;

public interface ITourExecutionService
{
    public Result<TourExecutionDto> Create(TourExecutionDto dto);
    public Result<TourExecutionDto> FinalizeTourExecution(int tourExecutionId, string status, int touristId);
}