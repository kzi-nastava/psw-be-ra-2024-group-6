using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Shopping
{
    public interface IPurchaseTokenService
    {
        public Result<List<PurchaseTokenDto>> GetByUserId(long id);
        public Result<PurchaseTokenDto?> GetByUserAndTour(long userId, long tourId);
    }
}
