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
    public interface IProblemService
    {
        Result<PagedResult<ProblemDto>> GetPaged(int page, int pageSize);
        Result<ProblemDto> Create(ProblemDto equipment);
        Result<ProblemDto> Update(ProblemDto equipment);
        Result Delete(int id);
        Result<PagedResult<ProblemDto>> GetByTourId(int tourid);
        Result<PagedResult<ProblemDto>> GetByTouristId(int touristid);
    }
}
