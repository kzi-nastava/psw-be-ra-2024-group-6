using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.DistanceDtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class RoadTripService : IRoadTripService
    {
        private readonly IRoadTripRepository _roadTripRepository;
        private readonly IMapper _mapper;
        private readonly ITourService _tourService;
        private readonly ICheckpointService _checkpointService;
        public RoadTripService(IRoadTripRepository roadTripRepository, IMapper mapper, ITourService tourService, ICheckpointService checkpointService)
        {
            _roadTripRepository = roadTripRepository;
            _mapper = mapper;
            _tourService = tourService;
            _checkpointService = checkpointService;
        }

        public Result<RoadTripReadDto> CreateRoadTrip(RoadTripCreateDto roadTripCreateDto, int userId)
        {
            var tours = new List<Tour>();
            foreach (long tourId in roadTripCreateDto.TourIds)
            {
                var tour = _tourService.GetById(tourId).Value;
                tours.Add(_mapper.Map<Tour>(tour));
            }

            var publicCheckpoints = new List<Checkpoint>();
            foreach (long checkpointId in roadTripCreateDto.PublicCheckpointIds)
            {
                var checkpoint = _checkpointService.Get(checkpointId).Value;
                publicCheckpoints.Add(_mapper.Map<Checkpoint>(checkpoint));
            }

            var roadTrip = new RoadTrip(userId, roadTripCreateDto.TourIds, roadTripCreateDto.PublicCheckpointIds);
            roadTrip.CalculateDifficulty(tours);
            roadTrip.CalculateTotalLength(tours);

            return _mapper.Map<RoadTripReadDto>(_roadTripRepository.Create(roadTrip));
        }

        public Result<List<RoadTripReadDto>> GetAllByTouristId(int touristId)
        {
            var roadTrips = _roadTripRepository.GetAllByTouristId(touristId);
            var roadTripDtos = new List<RoadTripReadDto>();
            foreach (var roadTrip in roadTrips)
            {
                var tours = _tourService.GetToursByIds(roadTrip.TourIds);
                var publicCheckpoints = _checkpointService.GetCheckpointsByIds(roadTrip.PublicCheckpointIds);
                roadTripDtos.Add(new RoadTripReadDto((int)roadTrip.Id, touristId, tours, publicCheckpoints, roadTrip.Difficulty.ToString(), _mapper.Map<DistanceDto>(roadTrip.TotalLength)));
            }
            return roadTripDtos;
        }

        public Result<RoadTripReadDto> GetById(int id)
        {
            var result = _roadTripRepository.Get(id);
            if (result == null)
            {
                return Result.Fail(FailureCode.NotFound).WithError("RoadTrip not found");
            }
            return _mapper.Map<RoadTripReadDto>(result);
        }
    }
}
