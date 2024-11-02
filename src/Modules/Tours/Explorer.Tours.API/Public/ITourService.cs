using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourService
    {
        Result<List<TourDto>> GetByUserId(long userId);
        Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
        Result<TourDto> Create(TourDto tour);
        Result<TourDto> Update(TourDto tour);
        Result Delete(int id);
        Result<TourCreateDto> CreateTour(TourCreateDto tour);
        Result<TourReadDto> PublishTour(long tourId, int userId);
        Result<TourReadDto> Archive(long tourId, int userId);

        //Result GetTourDetailsByTourId(int tourId);
    }
}
