using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos;
using FluentResults;

namespace Explorer.Tours.API.Internal
{
    public interface IInternalTourService
    {
        public Result<List<TourCardDto>> GetAllTourCards(int page, int pageSize);
        public Result<List<TourCardDto>> GetMostPopularTours(int count);
    }
}
