using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
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
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Tours.Core.UseCases
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ICrudRepository<Tour> crudRepository;
        private readonly ICheckpointService _checkpointService;
        private readonly IObjectService _objectService;
        private readonly IPersonService _personService;
        private readonly IMapper mapper;
        public TourService(ICrudRepository<Tour> repository, IMapper mapper,ITourRepository tourRepository, IObjectService objectService,ICheckpointService checkpointService, IPersonService personService) : base(repository, mapper)

        {
            _tourRepository = tourRepository;
            crudRepository = repository;
            _objectService = objectService;
            _checkpointService = checkpointService;
            _personService = personService;
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


        public Result<TourDetailsDto> GetTourDetailsByTourId(int tourId, int userId)
        {
            try
            {
                Tour tour = crudRepository.Get(tourId);
                if (!tour.IsAuthorOwner(userId))
                    return Result.Fail(FailureCode.Forbidden).WithError("You are not the author of this tour");
                TourDto tourDto = MapToDto(tour);

                List<CheckpointReadDto> checkpoints = _checkpointService.GetByTourId(tourId).Value;
                List<ObjectReadDto> objects = _objectService.GetByTourId(tourId).Value;
                TourDetailsDto tourDetailsDto = new TourDetailsDto
                {
                    TourInfo = tourDto,
                    Checkpoints = checkpoints,
                    Objects = objects
                };
                return Result.Ok(tourDetailsDto);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourReadDto> Publish(long tourId, int userId)
        {
            try
            {
                Tour tour = _tourRepository.GetAggregate(tourId);
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
                Tour tour = _tourRepository.GetAggregate(tourId);
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



        public Result<List<TourCardDto>> GetAllTourCards(int page, int pageSize)
        {
            PagedResult<Tour> tours = crudRepository.GetPaged(page, pageSize);

            List<TourCardDto> tourCardDtos = new List<TourCardDto>();

            foreach (Tour tour in tours.Results)
            {
                if (tour.Status == Status.Published)
                {
                    TourCardDto tourCardDto = new TourCardDto(tour.Id, tour.Name, tour.Price.Amount,
                         tour.TotalLenght.ToString());

                    tourCardDtos.Add(tourCardDto);
                }
            }

            return tourCardDtos;
        }

        /*public Result<List<TourCardDto>> GetAllTourCards()
        {
            List<Tour> tours = _tourRepository.GetToursWithReviews();

            List<TourCardDto> tourCardDtos = new List<TourCardDto>();

            foreach (Tour tour in tours)
            {
                if (tour.Status == Status.Published)
                {
                    double avg = tour.GetAverageRating();
                    TourCardDto tourCardDto = new TourCardDto(tour.Id, tour.Name, tour.Price.Amount, tour.TotalLength.ToString(), avg);
                    tourCardDtos.Add(tourCardDto);
                }
            }

            return tourCardDtos;
        }*/



        public Result<TourPreviewDto> GetTourPreview(long tourId)
        {
            Tour tour = crudRepository.Get(tourId);
            PersonDto author = _personService.GetByUserId((int)tour.AuthorId).Value;
            CheckpointReadDto firstCp = _checkpointService.GetByTourId(tour.Id).Value.First();
            List<string> durations = tour.Durations.Select(dur => dur.ToString()).ToList();
            TourPreviewDto tourPreviewDto = new TourPreviewDto(tour.Id, tour.Name, tour.Description,
                tour.Difficulty.ToString(), tour.Tags, tour.Price.Amount, author.Name + " " + author.Surname,
                tour.TotalLenght.ToString(), durations, firstCp);

            return tourPreviewDto;
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

    }
}
