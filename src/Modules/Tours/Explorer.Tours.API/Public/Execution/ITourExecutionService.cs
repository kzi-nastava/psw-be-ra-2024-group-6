using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Execution;

public interface ITourExecutionService
{
    public Result<TourExecutionDto> Create(TourExecutionDto dto);
    public Result<TourExecutionDto> FinalizeTourExecution(int tourExecutionId, string status, int touristId);
    public Result<ICollection<TourExecutionDto>> GetByTouristId(int touristId);

    public Result<TourExecutionDto> CompleteCheckpoint(int tourExecutionId,int checkpointId,int checkpointNum);
    public Result<TourExecutionDto> UpdateTourist(TourExecutionDto tourExecution);
    public long? GetTourIdByTourExecutionId(int tourExecutionId);
}