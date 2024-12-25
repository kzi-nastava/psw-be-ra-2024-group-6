using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Execution;

public class RoadTripExecutionService : BaseService<RoadTripExecutionDto, RoadTripExecution>, IRoadTripExecutionService
{
    private readonly IRoadTripExecutionRepository _roadTripExecutionRepository;
    private readonly IRoadTripRepository _roadTripRepository;
    private readonly ITourExecutionService _tourExecutionService;
    private readonly ITourExecutionRepository _tourExecutionRepository;

    public RoadTripExecutionService(IRoadTripExecutionRepository roadTripExecutionRepository, IRoadTripRepository roadTripRepository, ITourExecutionService tourExecutionService, ITourExecutionRepository tourExecutionRepository, IMapper mapper) : base(mapper)
    {
        _roadTripExecutionRepository = roadTripExecutionRepository;
        _roadTripRepository = roadTripRepository;
        _tourExecutionService = tourExecutionService;
        _tourExecutionRepository = tourExecutionRepository;
    }

    public Result<RoadTripExecutionDto> Create(RoadTripExecutionCreateDto roadTripExecutionDto, int userId)
    {
        try
        {

            var roadTrip = _roadTripRepository.Get(roadTripExecutionDto.RoadTripId);
            if (roadTrip == null)
            {
                return Result.Fail(FailureCode.NotFound).WithError("RoadTrip not found");
            }

            // ako je vec pokrenut road trip execution, ne moze se pokrenuti novi
            var isOneStarted = _roadTripExecutionRepository.IsOneStarted();
            if (isOneStarted)
            {
                return Result.Fail(FailureCode.Forbidden).WithSuccess("A road trip is already on going");
            }
            var tourExecutionIds = new List<int>();
            foreach (var tourId in roadTrip.TourIds)
            {
                var tourExecution = new TourExecutionDto();
                tourExecution.TourId = tourId;
                tourExecution.TouristId = userId;
                tourExecution.Longitude = roadTripExecutionDto.Longitude;
                tourExecution.Latitude = roadTripExecutionDto.Latitude;
                tourExecution.LastActivity = DateTime.UtcNow;

                var resultTourExecution = _tourExecutionService.Create(tourExecution);
                if (resultTourExecution.IsFailed)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument exception");
                }
                tourExecutionIds.Add(resultTourExecution.Value.Id);
            }
            var roadTripExecution = new RoadTripExecution(userId, roadTripExecutionDto.RoadTripId, 0, roadTripExecutionDto.Longitude, roadTripExecutionDto.Latitude);

            foreach(var tourExecutionId in tourExecutionIds)
            {
                roadTripExecution.TourExecutionIds.Add(tourExecutionId);
            }
            var result = _roadTripExecutionRepository.Create(roadTripExecution);
            return MapToDto(result);
        }
        catch(ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument exception");
        }
    }

    public Result<RoadTripExecutionDto> FinalizeRoadTripExecution(int roadTripExecutionId, string status, int touristId)
    {
        try
        {
            var roadTripExecution = _roadTripExecutionRepository.Get(roadTripExecutionId);
            roadTripExecution.Finalize(status);
            var updatedRoadTripExecution = _roadTripExecutionRepository.Update(roadTripExecution);
            return MapToDto(updatedRoadTripExecution);
        }
        catch(KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument exception");
        }
    }

    public Result<RoadTripExecutionDto> GetById(int roadTripExecutionId, int userId)
    {
        try
        {
            var roadTripExecution = _roadTripExecutionRepository.Get(roadTripExecutionId);
            int roadTripTouristId = _roadTripExecutionRepository.GetTouristId(roadTripExecutionId);
            if (roadTripTouristId != userId)
            {
                return Result.Fail(FailureCode.Forbidden).WithError("User is not allowed to access this resource");
            }
            return MapToDto(roadTripExecution);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<ICollection<RoadTripExecutionDto>> GetByTouristId(int touristId)
    {
        try
        {
            var roadTripExecutions = _roadTripExecutionRepository.GetByTouristId(touristId);
            return roadTripExecutions.Select(roadTripExecution => MapToDto(roadTripExecution)).ToList();
        }
        catch(Exception e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<RoadTripExecutionDto> FinalizeRoadTripExecution(int roadTripExecutionId, int userId, string status)
    {
        try
        {
            var roadTripExecution = _roadTripExecutionRepository.GetByIdAndTouristId(roadTripExecutionId, userId);
            if(roadTripExecution.Completion == 100)
            {
                roadTripExecution.Finalize(status);
            }
            else
            {
                return Result.Fail(FailureCode.Forbidden).WithError("Road trip cannot be completed");
            }
            var updatedRoadTripExecution = _roadTripExecutionRepository.Update(roadTripExecution);
            return MapToDto(updatedRoadTripExecution);
        }
        catch(KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<RoadTripExecutionDto> UpdateLocation(RoadTripExecutionDto roadTripExecutionDto)
    {
        try
        {
            var roadTripExecutionNew = _roadTripExecutionRepository.Get(roadTripExecutionDto.Id);
            roadTripExecutionNew.SetLastActivity(roadTripExecutionDto.Longitude, roadTripExecutionDto.Latitude);
            var tourExecutionIds = roadTripExecutionNew.TourExecutionIds;
            foreach(var id in tourExecutionIds)
            {
                _tourExecutionService.UpdateLocation(id, roadTripExecutionDto.Longitude, roadTripExecutionDto.Latitude);
            }
            var result = _roadTripExecutionRepository.Update(roadTripExecutionNew);
            return MapToDto(result);
        }
        catch(KeyNotFoundException e){
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<RoadTripExecutionDto> UpdateCompletion(int roadTripExecutionId)
    {
        try
        {
            var roadTripExecution = _roadTripExecutionRepository.Get(roadTripExecutionId);
            int completionCount = 0;
            foreach(var tourExecutionId in roadTripExecution.TourExecutionIds)
            {
                var tourExecution = _tourExecutionRepository.Get(tourExecutionId);
                if(tourExecution.Status == TourExecutionStatus.COMPLETED)
                {
                    completionCount++;
                }
            }
            if(completionCount > 0)
                roadTripExecution.CalculateCompletion(completionCount);

            var result = _roadTripExecutionRepository.Update(roadTripExecution);
            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}
