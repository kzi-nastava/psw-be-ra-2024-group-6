using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using TourCreateDto = Explorer.Tours.API.Dtos.TourDtos.TourCreateDto;

namespace Explorer.Tours.API.Public
{
    public interface ITourService
    {
        Result<List<TourAuthorCardDto>> GetByUserId(long userId);
        Result<TourDto> GetById(long tourId);
        Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);

        public Result<PagedResult<TourDto>> GetFilteredTours(int page, int pageSize, int userId);
        Result Delete(int id);
        Result<TourCreateDto> Create(TourCreateDto tour);
        Result<TourReadDto> Publish(long tourId, int userId);
        Result<TourReadDto> Archive(long tourId, int userId);
        Result<TourDto> Update(TourDto tour);

        Result<TourReadDto> GetTourDetailsByTourId(long tourId,long userId);
        Result<List<TourCardDto>> GetAllTourCards(int page, int pageSize);
        Result<TourPreviewDto> GetTourPreview(long tourId);
        Result<List<TourCardDto>> GetSearchedToursNearby(double latitude, double longitude, double radius);
        Result<List<TourHoverMapDto>> FindToursOnMapNearby(double latitude, double longitude, double radius);
        //Result GetTourDetailsByTourId(int tourId);


        Result<List<TourCardDto>> GetBoughtTours(long userId);
        Result <List<TourMapPreviewDto>> GetTourPreviewsOnMap(double latitude, double longitude);
        public Result<List<TourCardDto>> GetMostPopularTours(int count);
        public Result<List<DestinationTourDto>> GetToursForDestination(string city, string country, int page, int pageSize);
        Result<List<AuthorLeaderboardDto>> GetAuthorLeaderboard(double latitude, double longitude, double radius);
        Result<List<TourCardDto>> GetSortedTours(double latitude, double longitude, double radius,string criteria);
        Result<List<TourCardDto>> GetFilteredTours(double latitude, double longitude, double radius,TourFilterDto tourFiltersDto);
    }
}
