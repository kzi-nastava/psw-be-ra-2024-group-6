using Explorer.Tours.API.Dtos.TourDtos;
using FluentResults;

namespace Explorer.Tours.API.Public;

public interface IRoadTripService
{
    public Result<RoadTripReadDto> CreateRoadTrip(RoadTripCreateDto roadTripCreateDto, int userId);
    public Result<List<RoadTripReadDto>> GetAllByTouristId(int touristId);
    public Result<RoadTripReadDto> GetById(int id);
}