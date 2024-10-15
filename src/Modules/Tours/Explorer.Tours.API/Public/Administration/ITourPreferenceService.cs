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
    public interface ITourPreferenceService
    {
        Result<PagedResult<TourPreferenceDto>> GetPaged(int page, int pageSize);
        Result<TourPreferenceDto> Create(TourPreferenceDto tourPreference);
        Result<TourPreferenceDto> Update(TourPreferenceDto tourPreference);
        Result Delete(int id);
    }
}
