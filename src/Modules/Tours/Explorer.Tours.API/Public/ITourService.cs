using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourCreateDto = Explorer.Tours.API.Dtos.TourDtos.TourCreateDto;

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
        Result<TourDto> Update(TourDto tour);

        Result<TourReadDto> GetTourDetailsByTourId(int tourId,int userId);
        Result<List<TourCardDto>> GetAllTourCards(int page, int pageSize);
        Result<TourPreviewDto> GetTourPreview(long tourId);
        //Result GetTourDetailsByTourId(int tourId);
    }
}
