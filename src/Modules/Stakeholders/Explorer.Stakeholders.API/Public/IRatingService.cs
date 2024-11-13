using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IRatingService
    {
        Result<PagedResult<RatingDto>> GetPaged(int page, int pageSize);
        Result<RatingDto> Create(RatingDto rating);
        Result<RatingDto> Update(RatingDto rating);
        Result Delete(int id);
        Result<List<RatingReadDto>> GetBestPaged(int page, int pageSize);
    }
}
