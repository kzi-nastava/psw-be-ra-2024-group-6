using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Explorer.Tours.API.Public.Administration
{
    public interface ICheckpointService
    {
        Result<PagedResult<CheckpointDto>> GetPaged(int page, int pageSize);
        Result<CheckpointDto> Create(CheckpointDto equipment);
        Result<CheckpointDto> Update(CheckpointDto equipment);
        Result Delete(int id);
        Result<List<CheckpointDto>> GetByTourId(long tourId);
    }
}
