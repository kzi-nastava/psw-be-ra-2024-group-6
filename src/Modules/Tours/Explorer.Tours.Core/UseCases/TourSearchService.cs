using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointDtos;
using Explorer.Tours.API.Dtos.TourDtos.DistanceDtos;
using Explorer.Tours.API.Dtos.TourDtos.DurationDtos;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Utils;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class TourSearchService : ITourSearchService
    {
        private readonly IMapper mapper;
        private readonly ITourRepository _tourRepository;
        private readonly IReviewService _reviewService;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly IAuthorRecommenderService _authorRecommenderService;
        private readonly IInternalPersonService _personService;

        public TourSearchService(IMapper mapper,ITourRepository tourRepository,IReviewService reviewService,ICheckpointRepository checkpointRepository,IAuthorRecommenderService authorRecommenderService,IInternalPersonService personService)
        {
            this.mapper = mapper;
            _tourRepository = tourRepository;
            _reviewService = reviewService;
            _checkpointRepository = checkpointRepository;
            _authorRecommenderService = authorRecommenderService;
            _personService = personService;

        }
        public Result<List<TourMapPreviewDto>> GetTourPreviewsOnMap(double latitude, double longitude)
        {
            List<TourMapPreviewDto> toursOnSameLocationDtos = new List<TourMapPreviewDto>();
            List<Tour> toursOnSameLocation = GetNearbyVisibleTours(latitude, longitude, Location.GetTolerance());
            foreach (Tour tour in toursOnSameLocation)
            {
                PersonDto author = _personService.GetByUserId((int)tour.AuthorId).Value;
                toursOnSameLocationDtos.Add(new TourMapPreviewDto(tour.Id, author.Id, tour.Name, author.Surname,
                    tour.Difficulty.ToString(), tour.Price.Amount, tour.GetAverageRating(), /*tour.Image*/"gas",
                    tour.GetNumberOfReviews(), author.Name, author.PictureURL,
                    mapper.Map<List<TourDurationDto>>(tour.Durations), mapper.Map<DistanceDto>(tour.TotalLength)));
            }

            return toursOnSameLocationDtos;
        }
        public Result<List<TourCardDto>> GetSearchedToursNearby(double latitude, double longitude, double maxDistance)
        {
            try
            {
                List<Tour> nearbyTours = GetNearbyTours(latitude, longitude, maxDistance);

                List<TourCardDto> nearbyToursDto = new List<TourCardDto>();

                foreach (Tour tour in nearbyTours)
                {

                    double avg = tour.GetAverageRating();
                    TourCardDto tourCardDto = new TourCardDto(tour.Id, tour.Name, tour.Price.Amount,
                        mapper.Map<DistanceDto>(tour.TotalLength), avg, tour.Difficulty.ToString(),
                        tour.GetNumberOfReviews(), mapper.Map<List<TourDurationDto>>(tour.Durations));


                    nearbyToursDto.Add(tourCardDto);
                }

                return nearbyToursDto;
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
        public Result<List<TourCardDto>> GetSortedTours(double latitude, double longitude, double maxDistance,
    string criteria)
        {
            List<Tour> tours = GetNearbyTours(latitude, longitude, maxDistance);
            switch (criteria)
            {
                case "priceASC":
                    tours = tours.OrderBy(tour => tour.Price.Amount).ToList();
                    break;

                case "priceDESC":
                    tours = tours.OrderByDescending(tour => tour.Price.Amount).ToList();
                    break;

                case "ratingASC":
                    tours = tours.OrderBy(tour => tour.GetAverageRating()).ToList();
                    break;

                case "ratingDESC":
                    tours = tours.OrderByDescending(tour => tour.GetAverageRating()).ToList();
                    break;

                case "lengthASC":
                    tours = tours.OrderBy(tour => tour.TotalLength.Length).ToList();
                    break;

                case "lengthDESC":
                    tours = tours.OrderByDescending(tour => tour.TotalLength.Length).ToList();
                    break;

                default:
                    return Result.Fail("Bad request");
            }

            List<TourCardDto> tourCardDtos = new List<TourCardDto>();
            foreach (Tour tour in tours)
            {

                double avg = tour.GetAverageRating();
                TourCardDto tourCardDto = new TourCardDto(tour.Id, tour.Name, tour.Price.Amount,
                    mapper.Map<DistanceDto>(tour.TotalLength), avg, tour.Difficulty.ToString(),
                    tour.GetNumberOfReviews(), mapper.Map<List<TourDurationDto>>(tour.Durations));
                tourCardDtos.Add(tourCardDto);
            }

            return tourCardDtos;
        }

        public Result<List<TourCardDto>> GetFilteredTours(double latitude, double longitude, double radius,
            TourFilterDto tourFiltersDto)
        {
            List<Tour> tours = GetNearbyTours(latitude, longitude, radius);

            if (!string.IsNullOrEmpty(tourFiltersDto.Name))
            {
                tours = tours.Where(tour => tour.Name.Contains(tourFiltersDto.Name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (tourFiltersDto.MinPrice.HasValue)
            {
                tours = tours.Where(tour => tour.Price.Amount >= tourFiltersDto.MinPrice.Value).ToList();
            }

            if (tourFiltersDto.MaxPrice.HasValue)
            {
                tours = tours.Where(tour => tour.Price.Amount <= tourFiltersDto.MaxPrice.Value).ToList();
            }

            if (!string.IsNullOrEmpty(tourFiltersDto.Tag))
            {
                tours = tours.Where(tour => tour.Tags.Contains(tourFiltersDto.Tag)).ToList();
            }

            if (tourFiltersDto.MinLength.HasValue)
            {
                tours = tours.Where(tour => tour.TotalLength.Length >= tourFiltersDto.MinLength).ToList();
            }

            if (tourFiltersDto.MaxLength.HasValue)
            {
                tours = tours.Where(tour => tour.TotalLength.Length <= tourFiltersDto.MaxLength).ToList();
            }

            if (tourFiltersDto.MinDuration != null)
            {
                tours = tours.Where(tour => tour.Durations.Any(d =>
                    d.Duration >= tourFiltersDto.MinDuration.Duration &&
                    d.TransportType.ToString() == tourFiltersDto.MinDuration.TransportType)).ToList();
            }

            if (tourFiltersDto.MaxDuration != null)
            {
                tours = tours.Where(tour => tour.Durations.Any(d =>
                    d.Duration <= tourFiltersDto.MaxDuration.Duration &&
                    d.TransportType.ToString() == tourFiltersDto.MaxDuration.TransportType)).ToList();
            }

            List<TourCardDto> tourCardDtos = new List<TourCardDto>();
            foreach (Tour tour in tours)
            {

                double avg = tour.GetAverageRating();
                TourCardDto tourCardDto = new TourCardDto(tour.Id, tour.Name, tour.Price.Amount,
                    mapper.Map<DistanceDto>(tour.TotalLength), avg, tour.Difficulty.ToString(),
                    tour.GetNumberOfReviews(), mapper.Map<List<TourDurationDto>>(tour.Durations));
                tourCardDtos.Add(tourCardDto);
            }

            return tourCardDtos;
        }


        private List<Tour> GetNearbyTours(double latitude, double longitude, double maxDistance)
        {
            List<Tour> tours = _tourRepository.GetPublishedToursWithCheckpoints();
            return FindNearbyTours(tours, latitude, longitude, maxDistance);
        }

        private List<Tour> GetNearbyVisibleTours(double latitude, double longitude, double maxDistance)
        {
            List<Tour> tours = _tourRepository.GetPublishedToursWithCheckpoints();
            return FindNearbyVisibleTours(tours, latitude, longitude, maxDistance);
        }

        private List<Tour> FindNearbyTours(List<Tour> tours, double latitude, double longitude, double maxDistance)
        {
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

            return nearbyTours;
        }

        private List<Tour> FindNearbyVisibleTours(List<Tour> tours, double latitude, double longitude,
            double maxDistance)
        {
            List<Tour> nearbyTours = new List<Tour>();

            foreach (var tour in tours)
            {
                if (tour.IsTourVisibleNearby(latitude, longitude, maxDistance))
                {
                    tour.setReviews(mapper.Map<List<Review>>(_reviewService.GetReviewsFromTourId(tour.Id)));
                    nearbyTours.Add(tour);
                }

            }

            return nearbyTours;
        }
        public Result<List<TourHoverMapDto>> FindToursOnMapNearby(double latitude, double longitude, double maxDistance)
        {
            List<Tour> nearbyTours = GetNearbyTours(latitude, longitude, maxDistance);
            var uniqueLocations = new HashSet<string>();
            var nearbyToursDtos = new List<TourHoverMapDto>();

            foreach (Tour tour in nearbyTours)
            {
                var location = tour.GetPreviewCheckpoint().Location;
                string locationKey = $"{location.Latitude},{location.Longitude}";

                if (uniqueLocations.Add(locationKey))
                {
                    nearbyToursDtos.Add(CreateTourHoverMapDto(tour, true));
                }
                else
                {
                    MarkLocationAsNonUnique(nearbyToursDtos, location);
                }
            }
            return nearbyToursDtos;
        }

        private TourHoverMapDto CreateTourHoverMapDto(Tour tour, bool isLocationUnique)
        {
            return new TourHoverMapDto(
                mapper.Map<LocationReadDto>(tour.GetPreviewCheckpoint().Location),
                tour.Difficulty.ToString(),
                tour.GetAverageRating(),
                tour.Name,
                tour.Price.Amount,
                tour.Id,
                /*tour.Image*/ "gas",
                isLocationUnique
            );
        }

        private void MarkLocationAsNonUnique(List<TourHoverMapDto> tourDtos, Location location)
        {
            foreach (var dto in tourDtos)
            {
                if (dto.Location.Latitude == location.Latitude && dto.Location.Longitude == location.Longitude)
                {
                    dto.IsLocationUnique = false;
                    break;
                }
            }
        }

        public Result<RoadTripHoverMapDto> GetRoadTripSuggestions(RoadTripDto roadTripDto)
        {
            List<Tour> publishedToursInBbox = _tourRepository.GetPublishedToursWithCheckpointsInRectangle(roadTripDto.NorthEastCoord.Lat,roadTripDto.NorthEastCoord.Lng, roadTripDto.SouthWestCoord.Lat,roadTripDto.SouthWestCoord.Lng);
            List<Checkpoint> publicCheckpoints = _checkpointRepository.GetPublicCheckpointsInBox(roadTripDto.NorthEastCoord.Lat, roadTripDto.NorthEastCoord.Lng, roadTripDto.SouthWestCoord.Lat, roadTripDto.SouthWestCoord.Lng);
            roadTripDto.RoadCoords = RemoveUnnecessaryCoords(roadTripDto.RoadCoords);
            List<TourHoverMapDto> toursNearRoadDto = FindToursNearRoad(publishedToursInBbox,roadTripDto.RoadCoords,roadTripDto.Radius);
            List<CheckpointHoverMapDto> checkpointsNearRoadDto = FindCheckpointsNearRoad(publicCheckpoints,roadTripDto.RoadCoords, roadTripDto.Radius);
            return new RoadTripHoverMapDto(toursNearRoadDto, checkpointsNearRoadDto);
        }

        private List<TourHoverMapDto> FindToursNearRoad(List<Tour> tours ,List<LatLngDto>roadCoords,double roadRadius)
        {
            List<TourHoverMapDto> toursDto = new List<TourHoverMapDto>();
            foreach(Tour tour in tours)
            {
                if (IsCheckpointNearRoad(roadCoords,tour.GetPreviewCheckpoint(), roadRadius))
                {
                    toursDto.Add(CreateTourHoverMapDto(tour, true));
                }
            }

            return toursDto;
        }
        private bool IsCheckpointNearRoad(List<LatLngDto> roadCoords,Checkpoint checkpoint, double maxRadiusKm)
        {
            foreach (var roadCoord in roadCoords)
            {
                if (checkpoint.IsNearBy(roadCoord.Lat, roadCoord.Lng, maxRadiusKm))
                    return true;
            }

            return false;
        }
        private List<CheckpointHoverMapDto> FindCheckpointsNearRoad(List<Checkpoint> checkpoints, List<LatLngDto> roadCoords,double roadRadius)
        {
            List<CheckpointHoverMapDto> checkpointsNearRoadDto = new List<CheckpointHoverMapDto>();
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (IsCheckpointNearRoad(roadCoords,checkpoint,roadRadius))
                {
                    checkpointsNearRoadDto.Add(mapper.Map<CheckpointHoverMapDto>(checkpoint));
                }
            }

            return checkpointsNearRoadDto;
        }

        private List<LatLngDto> RemoveUnnecessaryCoords(List<LatLngDto> roadCoords)
        {
            List<LatLngDto> optimizedCoords = new List<LatLngDto>();
            foreach (LatLngDto roadCoord in roadCoords)
            {
                if (optimizedCoords.Count > 0)
                {
                    if (CoordIsClose(roadCoord,optimizedCoords.Last()))
                        continue;
                }
                optimizedCoords.Add(roadCoord);

            }

            return optimizedCoords;
        }

        private bool CoordIsClose(LatLngDto roadCoord,LatLngDto optimizedCoord)
        {
            return Utils.Utils.CalculateHaversineDistance(optimizedCoord.Lat, optimizedCoord.Lng, roadCoord.Lat,
                roadCoord.Lng) <= 0.5;
        }

        public Result<List<AuthorLeaderboardDto>> GetAuthorLeaderboard(double latitude, double longitude,
            double maxDistance)
        {
            List<Tour> nearbyTours = GetNearbyTours(latitude, longitude, maxDistance);
            List<long> authorIds = nearbyTours.Select(tour => tour.AuthorId).Distinct().ToList();
            return _authorRecommenderService.GetBestAuthors(authorIds, 5);
        }
    }
}
