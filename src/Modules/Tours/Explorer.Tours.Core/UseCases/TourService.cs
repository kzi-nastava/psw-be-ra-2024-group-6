using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class TourService : CrudService<TourDto,Tour>,ITourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ICrudRepository<Tour> crudRepository;
        private readonly ICheckpointService _checkpointService;
        private readonly IObjectService _objectService;
        private readonly IMapper mapper;

        public TourService(ICrudRepository<Tour> repository, IMapper mapper,ITourRepository tourRepository, IObjectService objectService,ICheckpointService checkpointService) : base(repository, mapper)
        {
            _tourRepository = tourRepository;
            crudRepository = repository;
            _objectService = objectService;
            _checkpointService = checkpointService;
            this.mapper = mapper;
        }

        public Result<TourCreateDto> CreateTour(TourCreateDto createTour)
        {
            try
            {
                Tour tour = mapper.Map<Tour>(createTour.TourInfo);
                Tour newTour = crudRepository.Create(tour);
                foreach (CheckpointCreateDto ch in createTour.Checkpoints)
                {
                    ch.TourId = newTour.Id;
                    _checkpointService.Create(mapper.Map<CheckpointCreateDto>(ch));
                }
                foreach (ObjectCreateDto o in createTour.Objects)
                {
                    o.TourId = newTour.Id;
                    _objectService.Create(mapper.Map<ObjectCreateDto>(o));
                }

                return Result.Ok(createTour);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }


        }

        public Result<List<TourDto>> GetByUserId(long userId)
        {
            try
            {
                var el = MapToDto(_tourRepository.GetByUserId(userId));
                return el;
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

        public Result<TourReadDto> PublishTour(long tourId, int userId)
        {
            try
            {
                Tour tour = _tourRepository.Get(tourId);
                if (!tour.IsUserAuthor(userId))
                    return Result.Fail("user is not author of tour");
                if (!tour.Publish())
                    return Result.Fail("publish failed");
                _tourRepository.Update(tour);
                return mapper.Map<TourReadDto>(tour);

            }
            catch (KeyNotFoundException ex)
            {
                return Result.Fail("tour not found");
            }


        }

        public Result<TourReadDto> Archive(long tourId, int userId)
        {
            try
            {
                Tour tour = _tourRepository.Get(tourId);
                if (!tour.IsUserAuthor(userId))
                    return Result.Fail("user is not author of tour");
                if (!tour.Archive())
                    return Result.Fail("publish failed");
                _tourRepository.Update(tour);
                return mapper.Map<TourReadDto>(tour);

            }
            catch (KeyNotFoundException ex)
            {
                return Result.Fail("tour not found");
            }
        }

        //public Result<TourDetailsDto> GetTourDetailsByTourId(long tourId)
        //{
            /*Tour tour = crudRepository.Get(tourId);

            TourDto tourDto = MapToDto(tour);

            List<CheckpointDto> Checkpoints = _checkpointService.GetByTourId(tourId).Value;
            List<ObjectDto> Objects = _objectService.GetByTourId(tourId).Value;

            foreach (CheckpointDto checkpointDto in Checkpoints)
            {
                new CheckpointReadDto(checkpointDto., checkpointDto.Name, checkpointDto.Description, checkpointDto.ImageUrl) 
            }
            */


            //}
    }
}
