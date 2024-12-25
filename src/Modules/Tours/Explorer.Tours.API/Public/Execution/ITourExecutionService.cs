using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Execution;

public interface ITourExecutionService
{
    public Result<TourExecutionDto> Create(TourExecutionDto tourExecution);
    public Result<TourExecutionDto> FinalizeTourExecution(int tourExecutionId, string status, int touristId);
    public Result<ICollection<TourExecutionDto>> GetByTouristId(int touristId);
    public Result<TourExecutionDto> Update(TourExecutionDto tourExecution);
    public Result<TourExecutionDto> GetById(int id);
    public Result<TourExecutionDto> CompleteCheckpoint(int tourExecutionId,int checkpointId,int checkpointNum);
    public Result<TourExecutionDto> UpdateTourist(TourExecutionDto tourExecution);
    public Result<TourExecutionDto> GetMostCompleted(int touristId, int tourId);
    public Result<TourExecutionDto> UpdateLocation(int tourExecutionId, double longitude, double latitude);
    public Result<List<CheckpointReadDto>> GetCompletedCheckpoints(int tourExecutionId);
    public Result<int> GetByTourId(int tourId, int userId);
    public int CalculateTourStartCount(long tourId);
    public int CalculateCompletedTourCount(long tourId);
    public int CountUniqueTourists(long tourId);
    public int CountUniqueTouristsForCheckpoint(long tourId, long checkpointId);

    public int CountAllStartedTours(List<long> tourIds);
    public int CountAllFinishedTours(List<long> tourIds);

    public List<TourExecutionDto> GetTourExecutionsWithMaxCompletionPerSale(List<long> tourIds);
}