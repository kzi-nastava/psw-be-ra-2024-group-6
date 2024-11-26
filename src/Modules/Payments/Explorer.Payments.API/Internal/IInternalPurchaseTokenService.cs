using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Internal
{
    public interface IInternalPurchaseTokenService
    {
        public Result<List<PurchaseTokenDto>> GetByUserId(long id);
        public Result<PurchaseTokenDto> Update(PurchaseTokenDto purchaseToken);
        public Result<PurchaseTokenDto> GetByUserAndTour(long userId, long tourId);
        public List<int> GetMostBoughtToursIds(int count);
    }
}
