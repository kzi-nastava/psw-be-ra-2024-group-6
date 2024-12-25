using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Public.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System.Diagnostics;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Public;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class CheckpointService : CrudService<CheckpointDto, Checkpoint>,ICheckpointService
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly IInternalNotificationService _notificationService;
        private readonly IMapper mapper;
        public CheckpointService(ICrudRepository<Checkpoint> crudRepository,ICheckpointRepository checkpointRepository, IInternalNotificationService notificationService, IMapper mapper) : base(crudRepository, mapper)
        {
            _notificationService = notificationService;
            _checkpointRepository = checkpointRepository;
            this.mapper = mapper;
        }


        Result<List<CheckpointReadDto>> ICheckpointService.GetByTourId(long tourId)
        {
            List<CheckpointReadDto> el = _checkpointRepository.GetByTourId(tourId).Select(mapper.Map<CheckpointReadDto>).ToList();
            return el;
        }
        public Result<CheckpointDto> Create(CheckpointCreateDto checkpointCreateDto)
        {
            try
            {
                return MapToDto(CrudRepository.Create(mapper.Map<Checkpoint>(checkpointCreateDto)));
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<CheckpointReadDto> CreatePublicCheckpoint(CheckpointDto checkpointCreateDto, long userId)
        {
            try
            {
                checkpointCreateDto.TourId = null;
                var checkpoint = mapper.Map<Checkpoint>(checkpointCreateDto); ;
                CrudRepository.Create(checkpoint);
                checkpoint.PublicRequest = new PublicCheckpointRequest(checkpoint.Id,  userId);
                CrudRepository.Update(checkpoint);
                var checkpointReadDto = mapper.Map<CheckpointReadDto>(checkpoint);

                return Result.Ok(checkpointReadDto);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<List<CheckpointDto>> GetNearbyPublicCheckpoints(double latitude, double longitude, double radius)
        {
            return FilterNearbyPublicCheckpoints(latitude, longitude, radius);
        }

        public List<CheckpointDto> FilterNearbyPublicCheckpoints(double latitude, double longitude, double radius)
        {
            var allPublicCheckpoints = _checkpointRepository.GetAllPublic();

            var nearbyCheckpoints = allPublicCheckpoints
                .Where(checkpoint =>
                    checkpoint.GetCheckpointDistance(latitude, longitude) <= radius)
                .ToList();


            return nearbyCheckpoints.Select(checkpoint =>
            {
                var dto = mapper.Map<CheckpointDto>(checkpoint);
                dto.Secret = null; // Postavljanje Secret na null
                return dto;
            }).ToList();
        }

        public Result<CheckpointDto> Get(long id) 
        {
            try
            {
                return MapToDto(_checkpointRepository.Get(id));
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);

            }
        }

        public Result<CheckpointReadDto> GetRead(long id)
        {
            try
            {
                return mapper.Map<CheckpointReadDto>(_checkpointRepository.Get(id));
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);

            }
        }

        public Result<List<CheckpointReadDto>> GetPendingPublicCheckpoints()
        {
            var pendingCheckpoints = _checkpointRepository.GetPendingPublicCheckpoints();
            var result = pendingCheckpoints.Select(mapper.Map<CheckpointReadDto>).ToList();
            return Result.Ok(result);
        }


        public Result<List<DestinationDto>> GetMostPopularDestinations()
        {
            var result = _checkpointRepository.GetMostPopularDestinations();
            return result
                .Select(ch => new DestinationDto(ch.Location.City, ch.Location.Country, ch.ImageData))
                .ToList();
        }

        public List<int> GetTourIdsForDestination(string city, string country, int page, int pageSize)
        {
            return _checkpointRepository.GetTourIdsForDestination(city, country, page, pageSize);
        }

        public Result<CheckpointReadDto> ApproveCheckpointRequest(long checkpointId, long adminId)
        {
            var checkpoint = _checkpointRepository.Get(checkpointId);
            checkpoint.ApprovePublicRequest();
            checkpoint.Update(checkpoint);
            _checkpointRepository.Update(checkpoint);


            var message = $"Your public checkpoint request '{checkpoint.Name}' has been approved.";
            _notificationService.SendPublicCheckpointRequestNotification(checkpoint.PublicRequest.UserId, message, adminId);

            return mapper.Map<CheckpointReadDto>(checkpoint);
        }


        public Result<CheckpointReadDto> RejectCheckpointRequest(long checkpointId, string comment, long adminId)
        {
            var checkpoint = _checkpointRepository.Get(checkpointId);
            checkpoint.RejectPublicRequest(comment);
            checkpoint.Update(checkpoint);
            _checkpointRepository.Update(checkpoint);


            var message = $"Your public checkpoint request '{checkpoint.Name}' has been rejected. Reason: {comment}";
            _notificationService.SendPublicCheckpointRequestNotification(checkpoint.PublicRequest.UserId, message, adminId);

            return mapper.Map<CheckpointReadDto>(checkpoint);
        }


        public List<CheckpointReadDto> GetCheckpointsByIds(List<int> checkpointIds)
        {
            var publicCheckpoints = new List<CheckpointReadDto>();
            foreach (long checkpointId in checkpointIds)
            {
                var checkpoint = _checkpointRepository.Get(checkpointId);
                publicCheckpoints.Add(mapper.Map<CheckpointReadDto>(checkpoint));
            }

            return publicCheckpoints;
        }
    }
}
