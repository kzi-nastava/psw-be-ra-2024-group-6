using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourSearchService
    {
        Result<List<TourMapPreviewDto>> GetTourPreviewsOnMap(double latitude, double longitude);
        Result<List<TourCardDto>> GetSearchedToursNearby(double latitude, double longitude, double radius);
        Result<List<TourHoverMapDto>> FindToursOnMapNearby(double latitude, double longitude, double radius);
        Result<List<AuthorLeaderboardDto>> GetAuthorLeaderboard(double latitude, double longitude, double radius);
        Result<List<TourCardDto>> GetSortedTours(double latitude, double longitude, double radius, string criteria);
        Result<List<TourCardDto>> GetFilteredTours(double latitude, double longitude, double radius, TourFilterDto tourFiltersDto);
        Result<RoadTripHoverMapDto> GetRoadTripSuggestions(RoadTripDto roadTripDto, double roadRadius=1);
    }
}
