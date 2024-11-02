using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration;

public interface IObjectService
{
    Result<PagedResult<ObjectDto>> GetPaged(int page, int pageSize);
    Result<ObjectDto> Create(ObjectDto equipment);
    Result<ObjectDto> Create(ObjectCreateDto objectCreateDto);
    Result<ObjectDto> Update(ObjectDto equipment);
    Result Delete(int id);
    Result<List<ObjectReadDto>> GetByTourId(long tourId);
}
