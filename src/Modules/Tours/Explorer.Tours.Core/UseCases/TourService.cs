using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
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
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Internal;

namespace Explorer.Tours.Core.UseCases
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ICrudRepository<Tour> crudRepository;
        private readonly ICheckpointService _checkpointService;
        private readonly IObjectService _objectService;
        private readonly IInternalTourPersonService _personService;
        private readonly IInternalPurchaseTokenService _tokenService;
        private readonly IReviewService _reviewService;
        private readonly IEquipmentRepository _equipmentRepository;

        private readonly IMapper mapper;
        public TourService(ICrudRepository<Tour> repository, IMapper mapper,ITourRepository tourRepository, IObjectService objectService, ICheckpointService checkpointService, IInternalTourPersonService personService, IInternalPurchaseTokenService token, IReviewService reviewService, IEquipmentRepository equipment) : base(repository, mapper)
        {
            _tourRepository = tourRepository;
            crudRepository = repository;
            _objectService = objectService;
            _checkpointService = checkpointService;
            _personService = personService;
            _tokenService = token;

            this.mapper = mapper;
            _reviewService = reviewService;
            _equipmentRepository = equipment;
        }

        public Result<PagedResult<TourDto>> GetFilteredTours(int page, int pageSize, int userId)
        {
            try
            {
                // Preuzimanje svih tura sa stranicama
                var toursResult = GetPaged(page, pageSize); // Poziva već postojeću metodu za stranicu
                if (toursResult.IsFailed)
                    return Result.Fail(toursResult.Errors);

                // Preuzimanje purchase tokena za korisnika
                var purchaseTokensResult = _tokenService.GetByUserId(userId);
                if (purchaseTokensResult.IsFailed)
                    return Result.Fail(purchaseTokensResult.Errors);

                // Logika za filtriranje tura koje je korisnik kupio
                var allTours = toursResult.Value;
                var purchasedTourIds = purchaseTokensResult.Value
                    .Where(pt => !pt.isExpired)
                    .Select(pt => pt.TourId)
                    .ToHashSet();

                var filteredTours = allTours.Results
                    .Where(tour => purchasedTourIds.Contains((int)tour.Id))
                    .ToList();

                var pagedResult = new PagedResult<TourDto>(filteredTours, filteredTours.Count);

                return Result.Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }


        public Result<TourCreateDto> Create(TourCreateDto createTour)
        {
            try
            {
                Tour tour = mapper.Map<Tour>(createTour.TourInfo);

                var equipmentIds = createTour.TourInfo.Equipment.Select(e => e.Id).ToList();
                var existingEquipment = _equipmentRepository.GetByIds(equipmentIds);

                if (existingEquipment.Count != equipmentIds.Count)
                {
                    return Result.Fail("One or more equipment items do not exist in the database.");
                }

                // Poveži postojeće Equipment entitete sa turom
                foreach(Equipment equipment in existingEquipment)
                {
                    tour.AddEquipment(equipment);
                }

                // Sačuvaj turu koristeći repozitorijum
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

        public Result<List<TourAuthorCardDto>> GetByUserId(long userId)
        {
            try
            {
                List<Tour> tours = _tourRepository.GetByUserId(userId);
                List<TourAuthorCardDto> tourAuthorCardDtos = new List<TourAuthorCardDto>();

                foreach (Tour tour in tours)
                {
                    double avg = tour.GetAverageRating();
                    TourAuthorCardDto tourAuthorCardDto = new TourAuthorCardDto(tour.Id, tour.Name, tour.Price.Amount, tour.TotalLength.ToString(), avg, tour.Status.ToString(), tour.StatusChangeTime);
                    tourAuthorCardDtos.Add(tourAuthorCardDto);
                }

                return tourAuthorCardDtos;
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
        public Result<TourDto> GetById(long tourId)
        {
            try
            {
                var el = MapToDto(_tourRepository.GetById(tourId));
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

        public Result<TourReadDto> GetTourDetailsByTourId(long tourId, long userId)
        {
            try
            {
                Tour tour = _tourRepository.GetByIdWithEquipment(tourId);
                if (!tour.IsUserAuthor(userId) && !checkIfUserBoughtTour(tourId, userId))
                    return Result.Fail(FailureCode.Forbidden).WithError("You are not authorized to view this tour.");
                TourDto tourDto = MapToDto(tour);

                List<CheckpointReadDto> checkpoints = _checkpointService.GetByTourId(tourId).Value;
                List<ObjectReadDto> objects = _objectService.GetByTourId(tourId).Value;
                TourReadDto tourDetailsDto = new TourReadDto
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

        private bool checkIfUserBoughtTour(long tourId, long userId)
        {

            var result = _tokenService.GetByUserAndTour(userId, tourId);
            return (result.Value != null);
        }

        public Result<List<TourCardDto>> GetBoughtTours(long userId)
        {
            try
            {
                List<TourCardDto> boughtTours = new List<TourCardDto>();

                foreach (var tour in GetAllTourCards(0,0).Value)
                {
                    if(checkIfUserBoughtTour(tour.Id, userId))
                        boughtTours.Add(tour);
                }

                return Result.Ok(boughtTours);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<TourCardDto>> GetMostPopularTours(int count)
        {
            try
            {
                var mostBoughtToursIds = _tokenService.GetMostBoughtToursIds(count);
                var mostBoughtTours = _tourRepository.GetAllByIds(mostBoughtToursIds);

                return mostBoughtTours.Select(tour => new TourCardDto(tour.Id, tour.Name, tour.Price.Amount, tour.TotalLength.ToString(), tour.GetAverageRating())).ToList();

            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<DestinationTourDto>> GetToursForDestination(string city, string country, int page, int pageSize)
        {
            try
            {
                var ids = _checkpointService.GetTourIdsForDestination(city, country, page, pageSize);
                var tours = _tourRepository.GetAllByIds(ids);
                var result = new List<DestinationTourDto>();

                foreach (var tour in tours)
                {
                    if (tour.IsNotPublished()) continue;
                    var firstCp = _checkpointService.GetByTourId(tour.Id).Value.First();
                    result.Add(new DestinationTourDto(tour.Name, tour.Description, tour.Difficulty.ToString(), tour.Price.Amount, tour.TotalLength.ToString(), firstCp));
                }

                return result;
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
            PagedResult<Tour> tours = _tourRepository.GetToursWithReviews(page, pageSize);

            List<TourCardDto> tourCardDtos = new List<TourCardDto>();

            foreach (Tour tour in tours.Results)
            {
                if (tour.Status == Status.Published)
                {
                    double avg = tour.GetAverageRating();
                    TourCardDto tourCardDto = new TourCardDto(tour.Id, tour.Name, tour.Price.Amount,tour.TotalLength.ToString(),avg);


                    tourCardDtos.Add(tourCardDto);
                }
            }

            return tourCardDtos;
        }
        


        public Result<TourPreviewDto> GetTourPreview(long tourId)
        {
            Tour tour = _tourRepository.GetTourWithReviews(tourId);
            PersonDto author = _personService.GetByUserId((int)tour.AuthorId).Value;
            CheckpointReadDto firstCp = _checkpointService.GetByTourId(tour.Id).Value.First();
            List<string> durations = tour.Durations.Select(dur => dur.ToString()).ToList();
            List<TourReviewDto> reviewDtos = GetTourReviewsDtos(tour.Reviews);
            TourPreviewDto tourPreviewDto = new TourPreviewDto(tour.Id, tour.Name, tour.Description,
                tour.Difficulty.ToString(), tour.Tags, tour.Price.Amount,author.UserId, author.Name + " " + author.Surname,author.PictureURL,
                tour.TotalLength.ToString(), durations, firstCp, reviewDtos);

            return tourPreviewDto;
        }

        private List<TourReviewDto> GetTourReviewsDtos(List<Review> reviews)
        {
            var reviewDtos = new List<TourReviewDto>();

            foreach (var review in reviews)
            {
                PersonDto reviewer = _personService.GetByUserId((int)review.TouristId).Value;

                var reviewDto = new TourReviewDto
                {
                    Id = review.Id,
                    UserId = reviewer.UserId,
                    Name = reviewer.Name,
                    Surname = reviewer.Surname,
                    Comment = review.Comment,
                    Rating = review.Rating, 
                    ReviewDate = review.ReviewDate,
                    AuthorImage = reviewer.PictureURL,
                    Images= review.Images
                };

                reviewDtos.Add(reviewDto);
            }

            return reviewDtos;
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


        public Result<List<TourCardDto>> FindToursNearby(double latitude, double longitude, double maxDistance)

        {
            try
            {
                List<Tour> tours = _tourRepository.GetPublishedToursWithCheckpoints();

                List<Tour> nearbyTours = new List<Tour>();

                foreach (var tour in tours)
                {
                    if (tour.IsTourNearby(latitude, longitude, maxDistance))
                    {
                        //nearbyToursDtos.Add(new TourCardDto(tour.Id, tour.Name, tour.Price.Amount, tour.TotalLength.ToString()));
                        tour.setReviews(mapper.Map<List<Review>>(_reviewService.GetReviewsFromTourId(tour.Id)));
                        nearbyTours.Add(tour);
                    }

                }

                List<TourCardDto> nearbyToursDto = new List<TourCardDto>();

                foreach (Tour tour in nearbyTours)
                {
                   
                        double avg = tour.GetAverageRating();
                        TourCardDto tourCardDto = new TourCardDto(tour.Id, tour.Name, tour.Price.Amount, tour.TotalLength.ToString(), avg);


                    nearbyToursDto.Add(tourCardDto);
                }

                return nearbyToursDto;
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

    }
}
