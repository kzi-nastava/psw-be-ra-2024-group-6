using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Execution;

public interface ITourExecutionService
{
    public Result<TourExecutionDto> Create(TourExecutionDto tourExecution);
    public Result<TourExecutionDto> FinalizeTourExecution(int tourExecutionId, string status, int touristId);
    public Result<ICollection<TourExecutionDto>> GetByTouristId(int touristId);
    public Result<TourExecutionDto> Update(TourExecutionDto tourExecution);
}