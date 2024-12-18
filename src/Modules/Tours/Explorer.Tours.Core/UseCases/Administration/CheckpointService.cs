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
        private readonly IMapper mapper;
        public CheckpointService(ICrudRepository<Checkpoint> crudRepository,ICheckpointRepository checkpointRepository, IMapper mapper) : base(crudRepository, mapper)
        {

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

        public Result<CheckpointReadDto> CreatePublicCheckpoint(CheckpointDto checkpointCreateDto)
        {
            try
            {
                checkpointCreateDto.TourId = null;
                var checkpoint = mapper.Map<Checkpoint>(checkpointCreateDto);
               // checkpoint.SetTourId(null);
                CrudRepository.Create(checkpoint);
                checkpoint.PublicRequest = new PublicCheckpointRequest(checkpoint.Id);
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
