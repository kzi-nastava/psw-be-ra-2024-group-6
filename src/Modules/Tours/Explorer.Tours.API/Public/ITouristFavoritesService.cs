using Explorer.Tours.API.Dtos.TourDtos;
using FluentResults;

namespace Explorer.Tours.API.Public;

public interface ITouristFavoritesService
{
    public Result<TouristFavoritesDto> GetByTouristId(int touristId);
}