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
        Result Delete(int id);
        Result<TourCreateDto> Create(TourCreateDto tour);
        Result<TourReadDto> Publish(long tourId, int userId);
        Result<TourReadDto> Archive(long tourId, int userId);

        //Result GetTourDetailsByTourId(int tourId);
    }
}
