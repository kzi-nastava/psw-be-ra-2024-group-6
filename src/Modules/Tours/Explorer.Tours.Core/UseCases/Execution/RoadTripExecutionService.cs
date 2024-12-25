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
            var existingRoadTripExecution = _roadTripExecutionRepository.GetByRoadTripId(roadTripExecutionDto.RoadTripId);
            if (existingRoadTripExecution != null)
            {
                return Result.Fail(FailureCode.Forbidden).WithError("RoadTrip already started");
            }

            foreach (var tourId in roadTrip.TourIds)
            {
                var tourExecution = new TourExecutionDto();
                tourExecution.TourId = tourId;
                tourExecution.TouristId = userId;
                tourExecution.Longitude = roadTripExecutionDto.Longitude;
                tourExecution.Latitude = roadTripExecutionDto.Latitude;
                tourExecution.LastActivity = DateTime.UtcNow;

                _tourExecutionService.Create(tourExecution);
            }
            var roadTripExecution = new RoadTripExecution(userId, roadTripExecutionDto.RoadTripId, 0, roadTripExecutionDto.Longitude, roadTripExecutionDto.Latitude);
            roadTripExecution.TourExecutionIds = roadTrip.TourIds;
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
}
